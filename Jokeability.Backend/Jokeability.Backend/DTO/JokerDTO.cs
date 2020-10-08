using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DTO
{
    public class JokerDTO
    {
        public int  Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isWithBadge { get; set; }
        public int TotalJokes { get; set; }
        public int FunnyJokes { get; set; }
        public int AwwJokes { get; set; }
        public double Rating { get; set; }
    }
}
