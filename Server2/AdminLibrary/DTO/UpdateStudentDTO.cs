using System;
using System.Collections.Generic;
using System.Text;

namespace AdminLibrary.DTO
{
    public class UpdateStudentDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
