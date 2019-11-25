using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLibrary.Data;
using AdminLibrary.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;

namespace AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminRepository repository;
        public AdminController(IAdminRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Admin
        [HttpGet("getTechnologies")]
        public IActionResult Get()
        {
            try
            {
                var technologies = repository.GetTechnologies();
                return Ok(new { Courses = technologies });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // [Authorize(Roles = "Admin")]
        // GET: api/Admin/5
        [HttpGet("GetTech/{id}", Name = "GetTechById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var tech = await repository.GetTechById(id);
                if (tech == null)
                {
                    return NotFound();
                }
                return Ok(tech);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // [Authorize(Roles = "Admin")]
        // POST: api/Admin/AddTech
        [Route("addTech")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TechnologyDTO technologyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool result = await repository.AddTechAsync(technologyDTO);
            if (result)
            {
                return Created("AddTech", "Technology added successfully");
            }
            return BadRequest(result);
        }

        // [Authorize(Roles = "Admin")]
        // PUT: api/Admin/UpdateTech
        [HttpPut("updateTech/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTechDTO updatedTechDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedTech = new Technology
            {
                TechId = updatedTechDTO.TechId,
                Name = updatedTechDTO.Name,
                Description = updatedTechDTO.Description,
                ImgSourceLink = updatedTechDTO.ImgSourceLink,
                BasicFee = updatedTechDTO.BasicFee,
                Commission = updatedTechDTO.Commission,
                Duration = updatedTechDTO.Duration,
                Status = updatedTechDTO.Status
            };

            bool result = await repository.UpdateTechAsync(updatedTech);
            if (result)
            {
                return Created("UpdateTech", new { Message = "Technology updated successfully", Data = updatedTechDTO });
            }
            return BadRequest(result);
        }


        // [Authorize(Roles = "Admin")]
        // DELETE: api/ApiWithActions/5
        [HttpDelete("deleteTech/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tech = repository.GetTechById(id);
            if (tech == null)
            {
                return NotFound();
            }
            var result = await repository.DeleteTechAsync(tech.Result);
            if (result)
            {
                return Created("DeleteTech", "Technology deleted successfully");
            }
            return BadRequest(result);
        }


        // [Authorize(Roles = "Admin")]
        // GET: api/Admin/GetMentors
        [HttpGet("getMentors")]
        public async Task<IActionResult> GetMentors()
        {
            try
            {
                var users = await repository.GetMentors();
                return Ok(new { users = users });
            }
            catch (Exception e)
            {
                throw;
            }
        }


        // [Authorize(Roles = "Admin")]
        // GET: api/Admin/GetMentors
        [HttpGet("getStudents")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var users = await repository.GetStudents();
                return Ok(new { users = users });
            }
            catch (Exception e)
            {
                throw;
            }
        }


        // [Authorize(Roles = "Admin")]
        // PUT: api/Admin/UpdateUser
        [HttpPut("updateUser/{email}")]
        public async Task<IActionResult> UpdateUser(string email, [FromBody] UpdateUserStatusDTO updateUserStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool result = await repository.UpdateUserStatus(updateUserStatus);
            if (result)
            {
                return Created("UpdateUser", new { Message = "User status updated successfully" });
            }
            return BadRequest(result);
        }


        // GET: api/Admin/GetCount
        [HttpGet("getCount")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var users = await repository.GetCount();
                return Ok(new { users = users });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // [Authorize(Roles = "Admin")]
        // GET: api/Admin/GetCourses
        [HttpGet("getCourses")]
        public IActionResult GetCourses()
        {
            try
            {
                return Ok(repository.GetCourses());
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}