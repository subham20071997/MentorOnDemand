using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorLibrary.Data;
using MentorLibrary.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MentorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        IMentorRepository mentorRepository;
        public MentorController(IMentorRepository mentorRepository)
        {
            this.mentorRepository = mentorRepository;
        }


        // GET: api/Mentor/GetSkills
        [HttpGet("getSkills/{email}")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                var skills = await mentorRepository.GetSkills(email);
                return Ok(new { Message = "Requested successful!", Skills = skills });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.InnerException.InnerException.ToString() });
            }
        }


        // GET: api/Mentor/5
        [HttpGet("getSkillData")]
        public IActionResult GetSkill([FromQuery] string email, int techId)
        {
            try
            {
                var skill = mentorRepository.GetSkill(email, techId);
                if (skill != null)
                {
                    return Ok(new { Message = "Request successful!", skill = skill });
                }
                return NotFound(new { Message = "Skill not found" });
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }


        // GET: api/Mentor/5
        [HttpGet("getTechnologies", Name = "GetTechnologies")]
        public IActionResult GetTechnologes()
        {
            try
            {
                var technologies = mentorRepository.GetTechnologies();
                return Ok(new { Courses = technologies });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.InnerException.InnerException.ToString() });
            }
        }


        // POST: api/Mentor/AddSkill
        [HttpPost("addSkill")]
        public IActionResult Post([FromBody] MentorSkillAddDTO addSkillDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = mentorRepository.AddSkill(addSkillDTO);
                if (result.Result == 1)
                {
                    return Created("AddSkill", new { Message = $"{addSkillDTO.Name} added to your skills successfully" });
                }
                else if (result.Result == 3)
                {
                    return BadRequest(new
                    {
                        Message = $"Duplicate skill entry, {addSkillDTO.Name} is already added to your skills. Check My Skills."
                    });
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        // PUT: api/Mentor/DeleteSkill/5
        [HttpDelete("deleteSkill")]
        public IActionResult DeleteSkill([FromQuery] int skillId, [FromQuery] string email)
        {
            try
            {
                var result = mentorRepository.DeleteSkill(skillId, email);
                if (result)
                {
                    return Ok(new { Message = "Skill deleted successfully." });
                }
                return BadRequest(new { Message = "Internal server error. Please try again later." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.InnerException.InnerException.ToString() });
            }
        }



        // PUT: api/Mentor/UpdateSkill/5
        [HttpPut("updateSkill")]
        public IActionResult Put([FromBody] MentorSkillAddDTO mentorSkillUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = mentorRepository.UpdateSkill(mentorSkillUpdateDTO);
                if (result.Result)
                {
                    return Created("UpdateSkill", new { Message = $"{mentorSkillUpdateDTO.Name} updated successfully" });
                }
                return BadRequest(new { Error = result, Message = $"Failed to update, {mentorSkillUpdateDTO.Name}. Please try again later!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Failed to update, {mentorSkillUpdateDTO.Name}. Please try again later!" });
            }
        }
    }
}