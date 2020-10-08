using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jokeability.Backend.DataContext.Models
{
    [Table("tblJokeStats",Schema ="dbo")]
    public class JokeStats
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int JokerID { get; set; }
        [Required]
        public int JokeID { get; set; }
        [Required]
        public int ReactionID { get; set; }
        [Required]
        public bool isActive { get; set; }


        [ForeignKey("JokeID")]
        public virtual Joke Joke { get; set; }
    }
}
