using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibrary.DTO
{
    public class StudentCourseRatingDTO
    {
        public string Email { get; set; }
        public int CourseId { get; set; }
        public int Rating { get; set; }
    }
}
