using Microsoft.AspNetCore.Identity;
using SharedLibrary.Models;
using StudentLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentLibrary.Data
{
    public class StudentRepository:IStudentRepository
    {
        StudentContext Context;
        private UserManager<MODUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public StudentRepository(StudentContext Context, UserManager<MODUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.Context = Context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public int AddCourse(StudentCourseAddDTO courseAddDTO)
        {
            try
            {
                // check if user with given email exists
                var student = Context.MODUser.SingleOrDefault(s => s.Email == courseAddDTO.Email);
                // check the role of the user
                //var studentRole = (from _student in Context.MODUser
                //                   join role in Context.UserRoles on _student.Id equals role.UserId
                //                   where _student.Email == courseAddDTO.Email
                //                   select role.RoleId).SingleOrDefault();
                //if (studentRole.Equals("2")) // Role Student
                    // check if student has already applied for this course
                    var isExists = (from user in Context.MODUser
                                    join studentCourse in Context.StudentCourses on user equals studentCourse.User
                                    join skill in Context.MentorSkills on studentCourse.SkillId equals skill.SkillId
                                    where user.Email == courseAddDTO.Email
                                    select studentCourse).SingleOrDefault();
                    if (isExists == null)
                    {
                        var course = new StudentCourse
                        {
                            User = student,
                            SkillId = courseAddDTO.SkillId,
                        };
                        Context.StudentCourses.Add(course);
                        var result = Context.SaveChanges();
                        if (result > 0)
                        {
                            return 1; // successfully applied for the course
                        }
                        return 2; // internal server error
                    }
                    return 3; // already applied for this course
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int CancelCourse(string email, int courseId)
        {
            try
            {
                var course = (from student in Context.MODUser
                              join studentCourse in Context.StudentCourses on student equals studentCourse.User
                              where student.Email == email && studentCourse.CourseId == courseId
                              select studentCourse).FirstOrDefault();
                if (course != null)
                {
                    // already completed or rejected
                    if (course.Status == 5 || course.Status == 7)
                    {
                        return 4;
                    }
                    course.Status = 6; // cancelled
                    var result = Context.SaveChanges();
                    if (result > 0)
                    {
                        return 1;
                    }
                    return 2; // server error cannot cancel
                }
                return 3; // not found


            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<StudentGetCourseMentorsDTO> GetCourseMentors(int techId)
        {
            try
            {
                var mentors = from tech in Context.Technologies
                              join skill in Context.MentorSkills on tech.TechId equals skill.TechId
                              where tech.TechId == techId
                              select (new StudentGetCourseMentorsDTO
                              {
                                  SkillId = skill.SkillId,
                                  MentorName = $"{skill.User.FirstName} {skill.User.LastName}",
                                  StartDate = skill.StartDate,
                                  EndDate = skill.EndDate,
                                  TotalFee = skill.TotalFee,
                                  TotalRating = 5,
                                  RatingsCount = 0, // to be fetched from users > skill.User.TotalRating, skill.User.RatingsCount
                                  Experience = 0
                              });
                return mentors;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IEnumerable<StudentCourseSendDTO> GetCourses(string email)
        {
            try
            {
                var courses = from course in Context.StudentCourses
                              join skill in Context.MentorSkills on course.SkillId equals skill.SkillId
                              join tech in Context.Technologies on skill.TechId equals tech.TechId
                              where course.User.Email == email
                              select (new StudentCourseSendDTO
                              {
                                  CourseId = course.CourseId,
                                  Name = tech.Name,
                                  MentorName = $"{skill.User.FirstName} {skill.User.LastName}",
                                  StartDate = skill.StartDate,
                                  EndDate = skill.EndDate,
                                  TotalFee = skill.TotalFee,
                                  Progress = course.Progress,
                                  Rating = course.Rating,
                                  Status = course.Status,
                                  PaymentStatus = course.PaymentStatus,
                                  PaymentId = course.PaymentId
                              });
                return courses;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UpdateCoursePayment(StudentCourseUpdatePaymentDTO updatePaymentDTO)
        {
            try
            {
                var course = (from studentCourse in Context.StudentCourses
                              join student in Context.MODUser on studentCourse.User equals student
                              where student.Email == updatePaymentDTO.Email && studentCourse.CourseId == updatePaymentDTO.CourseId
                              select studentCourse).FirstOrDefault();
                if (course != null)
                {
                    if (course.PaymentStatus)
                    {
                        return 4;
                    }
                    else if (course.Status == 6 || course.Status == 7)
                    {
                        return 5; // cancelled or rejected
                    }
                    course.PaymentStatus = true;
                    course.PaymentId = updatePaymentDTO.PaymentId;
                    course.Status = 3;
                    Context.StudentCourses.Update(course);
                    var result = Context.SaveChanges();
                    if (result > 0)
                    {
                        return 1; // success
                    }
                    return 2; // error updating payment details
                }
                return 3; // course not found or payment already completed
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UpdateCourseRating(StudentCourseRatingDTO courseRatingDTO)
        {
            try
            {
                var course = (from studentCourse in Context.StudentCourses
                              join student in Context.MODUser on studentCourse.User equals student
                              where student.Email == courseRatingDTO.Email && studentCourse.CourseId == courseRatingDTO.CourseId
                              select studentCourse).FirstOrDefault();
                if (course != null)
                {
                    if (course.Rating < 6)
                    {
                        return 4; // already rated
                    }
                    else if (course.Status != 5)
                    {
                        return 5; // course not completed yet
                    }
                    course.Rating = courseRatingDTO.Rating;
                    Context.StudentCourses.Update(course);
                    var result = Context.SaveChanges();
                    if (result > 0)
                    {
                        return 1; // success
                    }
                    return 2; // error updating status failed
                }
                return 3; // course not found
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
