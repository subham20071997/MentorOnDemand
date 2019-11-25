using MentorLibrary.DTO;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorLibrary.Data
{
    public class MentorRepository:IMentorRepository
    {
        MentorContext Context;
        private UserManager<MODUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public MentorRepository(MentorContext Context, UserManager<MODUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.Context = Context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<int> AddSkill(MentorSkillAddDTO skillAddDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(skillAddDTO.Email);
                var tech = Context.Technologies.Find(skillAddDTO.TechID);
                if (user == null || tech == null)
                {
                    return 2;
                }
                var isDublicate = Context.MentorSkills.Where(ms => ms.User == user && ms.TechId == tech.TechId).FirstOrDefault();
                if (isDublicate == null)
                {
                    MentorSkill skill = new MentorSkill
                    {
                        User = user,
                        TechId = tech.TechId,
                        StartDate = skillAddDTO.StartDate,
                        EndDate = skillAddDTO.EndDate,
                        SkillSurcharge = skillAddDTO.SkillSurcharge,
                        TotalFee = Convert.ToDecimal(tech.BasicFee * (1 + 0.01 * (tech.Commission + skillAddDTO.SkillSurcharge))),
                        Rating = 5,
                        RatingsCount = 0,
                        Status = true
                    };
                    Context.MentorSkills.Add(skill);
                    int result = Context.SaveChanges(); // returns number of changes
                    if (result > 0)
                    {
                        return 1; // success
                    }
                    return 2; // error
                }
                else
                {
                    return 3; // duplicate request
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool DeleteSkill(int id, string email)
        {
            try
            {
                var user = Context.MODUser.Where(u => u.Email == email).FirstOrDefault();
                var skill = Context.MentorSkills.Where(s => s.SkillId == id && s.User == user).FirstOrDefault();
                Context.MentorSkills.Remove(skill);
                var result = Context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public MentorSkillGetDTO GetSkill(string email, int techId)
        {
            try
            {
                var res = (from user in Context.MODUser
                           join skill in Context.MentorSkills on user equals skill.User
                           join tech in Context.Technologies on skill.TechId equals tech.TechId
                           where user.Email == email && tech.TechId == techId
                           select new MentorSkillGetDTO
                           {
                               Email = user.Email,
                               SkillId = skill.SkillId,
                               TechId = tech.TechId,
                               Name = tech.Name,
                               Rating = skill.Rating,
                               RatingsCount = skill.RatingsCount,
                               SkillSurcharge = skill.SkillSurcharge,
                               TotalFee = skill.TotalFee,
                               StartDate = skill.StartDate,
                               EndDate = skill.EndDate,
                               Status = skill.Status
                           }).FirstOrDefault();
                Console.WriteLine(res);
                return res;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<MentorSkillGetDTO>> GetSkills(string email)
        {
            try
            {
                var role = await userManager.GetUsersInRoleAsync("Mentor");
                var user = role.Where(r => r.Email == email).FirstOrDefault();
                if (user != null)
                {
                    var skills = Context.MODUser.Where(s => s.Email == user.Email)
                        .Select(ms => new MentorSkillGetDTO
                        {
                            Email = user.Email,
                            // SkillId = ms.Skills,
                            //  TechId = ms.TechId,
                            // Name = Context.Technologies.Where(tech => tech.TechId == ms.TechId).FirstOrDefault().Name,
                            // Rating = ms.Rating,
                            // RatingsCount = ms.RatingsCount,
                            // SkillSurcharge = ms.SkillSurcharge,
                            // TotalFee = ms.TotalFee,
                            // StartDate = ms.StartDate,
                            // EndDate = ms.EndDate,
                            Status = ms.Status
                        });
                    return skills;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<Technology> GetTechnologies()
        {
            try
            {
                // only return technologies which are active and not blocked by admin
                return Context.Technologies.Where(t => t.Status == true).ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> UpdateSkill(MentorSkillAddDTO updateSkillDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(updateSkillDTO.Email);
                var tech = Context.Technologies.Find(updateSkillDTO.TechID);
                var skill = Context.MentorSkills.Where(skill => skill.TechId == updateSkillDTO.TechID).FirstOrDefault();
                if (user == null || skill == null)
                {
                    return false;
                }

                skill.StartDate = updateSkillDTO.StartDate;
                skill.EndDate = updateSkillDTO.EndDate;
                skill.SkillSurcharge = updateSkillDTO.SkillSurcharge;
                skill.TotalFee = Convert.ToDecimal(tech.BasicFee * (1 + 0.01 * (tech.Commission + updateSkillDTO.SkillSurcharge)));

                int result = Context.SaveChanges(); // returns number of changes
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
