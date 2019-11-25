using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdminLibrary.DTO
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int Rating { get; set; }

        public string Skills { get; set; }
        public bool Status { get; set; }
    }
}
