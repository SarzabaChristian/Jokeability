using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DTO
{
    public class NewJokeDTO
    {
        [Required(ErrorMessage ="Unknown source of joke/ joker id must be filled up")]
        public int JokerID { get; set; }
        [Required(ErrorMessage = "Title must be filled up")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description must have value")]
        public string Description { get; set; }        
    }
}
