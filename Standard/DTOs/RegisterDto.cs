using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
