using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jokeability.Backend.DataContext.Models
{
    [Table("tblUsers",Schema ="dbo")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isWithBadge { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return this.LastName + ", " + this.FirstName; }
        }
   
    }
}
