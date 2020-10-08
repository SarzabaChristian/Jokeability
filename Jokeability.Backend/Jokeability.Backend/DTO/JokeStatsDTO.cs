using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DTO
{
    public class JokeStatsDTO
    {
        [Required(ErrorMessage ="User Id is required field")]
        [Range(1,int.MaxValue,ErrorMessage ="Invalid User ID")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Joke Id is required field")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Joke ID")]
        public int JokeID { get; set; }
        [Required(ErrorMessage = "Joker ID is required field")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Joker ID")]
        public int JokerID { get; set; }
        [Required(ErrorMessage = "Reaction is required field")]
        public string Reaction { get; set; }
    }
}
