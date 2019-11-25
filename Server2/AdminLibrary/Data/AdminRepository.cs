using AdminLibrary.DTO;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLibrary.Data
{
    public class AdminRepository:IAdminRepository
    {
        AdminContext context;
        private UserManager<MODUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public AdminRepository(AdminContext context, UserManager<MODUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<bool> AddTechAsync(TechnologyDTO technologyDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(technologyDTO.UserEmail);
                if (user == null)
                {
                    return false;
                }
                Technology technology = new Technology
                {
                    Name = technologyDTO.Name,
                    Description = technologyDTO.Description,
                    ImgSourceLink = technologyDTO.ImgSourceLink,
                    BasicFee = technologyDTO.BasicFee,
                    Commission = technologyDTO.Commission,
                    Duration = technologyDTO.Duration,
                    User = user
                };
                context.Technologies.Add(technology);

                int result = context.SaveChanges(); // returns number of changes
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

        public async Task<bool> DeleteTechAsync(Technology technology)
        {
            // pending
            try
            {
                context.Technologies.Remove(technology);
                var result = await context.SaveChangesAsync();
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

        public async Task<Technology> GetTechById(int id)
        {
            try
            {
                return await context.Technologies.FindAsync(id);
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
                return context.Technologies.ToList();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UpdateUserDTO>> GetMentors()
        {
            try
            {
                //var users = await userManager.GetUsersInRoleAsync(
                //    roleManager.Roles.SingleOrDefault(r => r.Id == roleId.ToString()).NormalizedName);
                var users = await userManager.GetUsersInRoleAsync("Mentor");
                var result = users.Select(u => new UpdateUserDTO
                {
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email,
                    Skills = u.Skills,
                    Status = u.Status
                });
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<IEnumerable<UpdateStudentDTO>> GetStudents()
        {
            try
            {
                //var users = await userManager.GetUsersInRoleAsync(
                //    roleManager.Roles.SingleOrDefault(r => r.Id == roleId.ToString()).NormalizedName);
                var users = await userManager.GetUsersInRoleAsync("Student");
                var result = users.Select(u => new UpdateStudentDTO
                {
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email,
                    Status = u.Status
                });
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> UpdateTechAsync(Technology technology)
        {
            try
            {
                context.Technologies.Update(technology);
                int result = await context.SaveChangesAsync();
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

        public async Task<bool> UpdateUserStatus(UpdateUserStatusDTO updateUserStatus)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(updateUserStatus.Email);
                user.Status = !user.Status;
                var result = context.SaveChanges();
                if (result == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        //get count of users..
        public async Task<CountDTO> GetCount()
        {
            try
            {
                var mentor = await userManager.GetUsersInRoleAsync("Mentor");
                var student = await userManager.GetUsersInRoleAsync("Student");
                var course = GetTechnologies();

                var countDto = new CountDTO
                {
                    MentorCount = mentor.Count(),
                    StudentCount = student.Count(),
                    CourseCount = course.Count()
                };

                return countDto;
            }
            catch (Exception e)
            {

                throw;
            }
        }


        public IEnumerable<Technology> GetCourses()
        {
            return context.Technologies.ToList();
        }

    }
}
