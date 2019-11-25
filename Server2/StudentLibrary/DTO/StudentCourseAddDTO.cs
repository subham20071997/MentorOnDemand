using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibrary.DTO
{
    public class StudentCourseAddDTO
    {
        // [Required]
        public string Email { get; set; }
        // [Required]
        public int SkillId { get; set; }
    }
}
