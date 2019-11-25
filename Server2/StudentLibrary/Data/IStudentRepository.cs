using StudentLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibrary.Data
{
    public interface IStudentRepository
    {
        public int AddCourse(StudentCourseAddDTO courseAddDTO);
        public IEnumerable<StudentCourseSendDTO> GetCourses(string email);
        public IEnumerable<StudentGetCourseMentorsDTO> GetCourseMentors(int techId);
        public int UpdateCoursePayment(StudentCourseUpdatePaymentDTO updatePaymentDTO);
        public int CancelCourse(string email, int courseId);
        public int UpdateCourseRating(StudentCourseRatingDTO courseRatingDTO);
    }
}
