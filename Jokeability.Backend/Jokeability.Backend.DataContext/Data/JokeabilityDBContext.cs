using Jokeability.Backend.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jokeability.Backend.DataContext.Data
{
    public class JokeabilityDBContext:DbContext
    {
        public JokeabilityDBContext(DbContextOptions<JokeabilityDBContext> options):base(options){ }
        public DbSet<User> Users { get; set; }
        public DbSet<Joke> Jokes { get; set; }
        public DbSet<JokeStats> JokeStats { get; set; }
        public DbSet<MasterSetting> MasterSettings { get; set; }
    }
}
