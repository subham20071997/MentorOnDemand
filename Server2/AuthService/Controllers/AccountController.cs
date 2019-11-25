using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthLibrary.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<MODUser> signInManager;
        private readonly UserManager<MODUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AccountController(SignInManager<MODUser> signInManager, UserManager<MODUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = userManager.Users.Single(r => r.Email == model.Email);
                var response = await GenerateJwtToken(model.Email, appUser);
                return Ok(response);
            }
            return BadRequest(result);
        }

        [Route("logout")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Logout Failed!");
            }
            return Ok();
        }


        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new MODUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // role name
                var roleName = roleManager.Roles.
                    FirstOrDefault(r => r.Id == model.Role.ToString()).NormalizedName;
                var res = await userManager.AddToRoleAsync(user, roleName);
                if (res.Succeeded)
                {
                    return Created("Registered", new { Email = model.Email });
                }
                return BadRequest(res.Errors);
            }
            return BadRequest(result.Errors);
        }

        private async Task<TokenDTO> GenerateJwtToken(string email, MODUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var role = roleManager.Roles.SingleOrDefault(r => r.Name == roles.SingleOrDefault());
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role,role.Name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // recommended is 5 min
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            //var roles = await userManager.GetRolesAsync(user);
            //var roleId = roleManager.Roles.SingleOrDefault(r => r.Name == roles.SingleOrDefault()).Id;
            var response = new TokenDTO
            {
                Email = email,
                Role = Convert.ToInt32(role.Id),
                Token = (new JwtSecurityTokenHandler()).WriteToken(token)
            };
            return response;
        }

       
        //[Authorize(Roles = "Student")]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetProfile(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            try
            {
                var user = userManager.Users.SingleOrDefault(u => u.Email == email);
                if (user == null)
                {
                    return NotFound($"User does not exist for email: {email}");
                }
                var role = await userManager.GetRolesAsync(user);
                var profileDto = new UserProfileDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Role = role.SingleOrDefault()
                };
                return Ok(profileDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[Authorize(Roles = "Student")]
        [Route("updateProfile/{email}")]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(string email, [FromBody] UserProfileDTO profileDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var user = userManager.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    user.FirstName = profileDTO.FirstName;
                    user.LastName = profileDTO.LastName;
                    user.Gender = profileDTO.Gender;
                    user.DateOfBirth = profileDTO.DateOfBirth;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Created("Register",
                            new { Message = "User updated successfully registered", user = user });
                    }
                    return BadRequest(result.Errors);
                }
                return NotFound(profileDTO);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        //[Authorize(Roles = "Mentor")]
        //get mentorprofile
        [HttpGet("getmentor/{email}")]
        public async Task<IActionResult> GetMentorProfile(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            try
            {
                var user = userManager.Users.SingleOrDefault(u => u.Email == email);
                if (user == null)
                {
                    return NotFound($"User does not exist for email: {email}");
                }
                var role = await userManager.GetRolesAsync(user);
                var profileDto = new MentorProfileDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Experience = user.Experience,
                    Linkedinprofile = user.Linkedinprofile,
                    Skills = user.Skills,
                    Role = role.SingleOrDefault()
                };
                return Ok(profileDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // [Authorize(Roles = "Mentor")]
        //update mentor profile
        [Route("updatementorProfile/{email}")]
        [HttpPut]
        public async Task<IActionResult> PutMentorProfile(string email, [FromBody] MentorProfileDTO profileDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var user = userManager.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    user.FirstName = profileDTO.FirstName;
                    user.LastName = profileDTO.LastName;
                    user.Gender = profileDTO.Gender;
                    user.DateOfBirth = profileDTO.DateOfBirth;
                    user.Experience = profileDTO.Experience;
                    user.Linkedinprofile = profileDTO.Linkedinprofile;
                    user.Skills = profileDTO.Skills;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Created("Register",
                            new { Message = "User updated successfully registered", user = user });
                    }
                    return BadRequest(result.Errors);
                }
                return NotFound(profileDTO);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        // [Authorize(Roles = "Admin")]
        //get admin details
        [HttpGet("getadmin/{email}")]
        public async Task<IActionResult> GetAdminProfile(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            try
            {
                var user = userManager.Users.SingleOrDefault(u => u.Email == email);
                if (user == null)
                {
                    return NotFound($"User does not exist for email: {email}");
                }
                var role = await userManager.GetRolesAsync(user);

                var profileDto = new AdminProfileDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Experience = user.Experience,
                    Linkedinprofile = user.Linkedinprofile,
                    Role = role.SingleOrDefault()
                };
                return Ok(profileDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[Authorize(Roles = "Admin")]
        //update admin profile
        [Route("updateadminProfile/{email}")]
        [HttpPut]
        public async Task<IActionResult> PutadminProfile(string email, [FromBody] AdminProfileDTO profileDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var user = userManager.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    user.FirstName = profileDTO.FirstName;
                    user.LastName = profileDTO.LastName;
                    user.Gender = profileDTO.Gender;
                    user.DateOfBirth = profileDTO.DateOfBirth;
                    user.Experience = profileDTO.Experience;
                    user.Linkedinprofile = profileDTO.Linkedinprofile;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Created("Register",
                            new { Message = "User updated successfully registered", user = user });
                    }
                    return BadRequest(result.Errors);
                }
                return NotFound(profileDTO);
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}