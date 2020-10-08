using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DTO
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Please fill out the username")]       
        public string Username { get; set; }
        [Required(ErrorMessage = "Please fill out the password")]
        public string Password { get; set; }
    }
}
