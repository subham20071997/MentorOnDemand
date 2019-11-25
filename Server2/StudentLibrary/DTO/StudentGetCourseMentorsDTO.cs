using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibrary.DTO
{
    public class StudentGetCourseMentorsDTO
    {
        public int SkillId { get; set; }
        public string MentorName { get; set; }
        public decimal TotalRating { get; set; }
        public int RatingsCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalFee { get; set; }
        public int Experience { get; set; }
    }
}
