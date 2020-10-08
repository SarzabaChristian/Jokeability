using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Jokeability.Backend.DataContext.Models
{
    [Table("tblJokes",Schema ="dbo")]
    public class Joke
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int JokerID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }        
        [Required]
        public bool isActive { get; set; }

        //[NotMapped]
        [ForeignKey("JokerID")]
        public User Joker { get; set; }        
    }
}
