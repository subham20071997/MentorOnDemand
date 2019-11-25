using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthLibrary.DTO
{
    public class AdminProfileDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public String LastName { get; set; }
        public Gender Gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public int Experience { get; set; }

        public string Linkedinprofile { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
