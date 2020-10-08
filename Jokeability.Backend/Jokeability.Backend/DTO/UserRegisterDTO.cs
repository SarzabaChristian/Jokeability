using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DTO
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage ="Username is required")]      
        [MinLength(4,ErrorMessage ="Username must have 4 or more characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Password must have 4 or more characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        
    }
}
