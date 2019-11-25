﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MentorLibrary.DTO
{
    public class MentorSkillAddDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int TechID { get; set; }
        public decimal Rating { get; set; }
        //[Required]
        public int SkillSurcharge { get; set; } = 0; // Range = [0 - 100]
        //[Required]
        public decimal TotalFee { get; set; } // TotalFee = BasicFee * ((100 + Commission + MentorSkillSurcharge)/100)
                                              // [Required]
        public DateTime StartDate { get; set; }
        // [Required]
        public DateTime EndDate { get; set; }
    }
}
