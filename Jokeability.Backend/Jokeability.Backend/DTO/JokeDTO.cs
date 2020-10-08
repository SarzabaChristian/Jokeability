using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DTO
{
    public class JokeDTO
    {
        public int Id { get; set; }
        public int JokerID { get; set; }
        public string JokerName { get; set; }
        public bool withBadge { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
    }
}
