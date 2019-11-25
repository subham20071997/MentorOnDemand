using System;
using System.Collections.Generic;
using System.Text;

namespace AuthLibrary.DTO
{
    public class TokenDTO
    {
        public string Email { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
    }
}
