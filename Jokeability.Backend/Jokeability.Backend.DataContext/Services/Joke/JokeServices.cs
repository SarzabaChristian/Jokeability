using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jokeability.Backend.DataContext.Data;
using Jokeability.Backend.DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace Jokeability.Backend.DataContext.Services
{
    public class JokeServices : IJokeServices
    {
        private readonly JokeabilityDBContext _context;
        public JokeServices(JokeabilityDBContext context)
        {
            _context = context;
        }
        public async Task<Joke> Add(Joke newJoke)
        {
            _context.Add(newJoke);
            var isSaved= await ProcessSave();
            if (isSaved)
                newJoke = _context.Jokes.Include(x => x.Joker).FirstOrDefault(x => x.Id == newJoke.Id);

            return newJoke;
        }

        public async Task<User> GetJoker(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Joke>> GetJokes()
        {
            return await _context.Jokes.Where(x=>x.isActive==true)
                                       .Include(x=>x.Joker)
                                       .OrderByDescending(x=>x.Id)
                                       .ToListAsync();
        }

        public async Task<int> GetReactionID(string reaction)
        {
             var masterSetting=await _context.MasterSettings.FirstOrDefaultAsync(x => x.Value == reaction
                                                                     && x.isActive == true
                                                                     && x.Group == "VoteCategory");      
            return masterSetting != null? masterSetting.Id:0;
        }

        public async Task<double> JokerRating(int jokerId)
        {
            double rating = 0;
            try
            {
                var jokesWithReaction = await _context.JokeStats.Where(x => x.JokerID == jokerId).Include(x => x.Joke).ToListAsync();
                var funnyReactionID = _context.MasterSettings.FirstOrDefault(x => x.isActive == true && x.Group == "VoteCategory" && x.Value == "Funny").Id;
                var funnyJokes = jokesWithReaction.Where(x => x.ReactionID == funnyReactionID).Count();
                var awwJokes = jokesWithReaction.Where(x => x.ReactionID != funnyReactionID).Count();
                rating = ((Convert.ToDouble(funnyJokes) / Convert.ToDouble((funnyJokes + awwJokes))) * 100);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }            
            return rating;
        }
        public void GetRatings(int jokerId, out int totalJokes,out int funnyJokes, out int awwJokes, out double rating)
        {
            var jokesWithReaction = _context.JokeStats.Where(x => x.JokerID == jokerId).Include(x => x.Joke).ToList();
            var funnyReactionID = _context.MasterSettings.FirstOrDefault(x => x.isActive == true && x.Group == "VoteCategory" && x.Value == "Funny").Id;
            totalJokes = _context.Jokes.Where(x => x.JokerID == jokerId).Count();
            funnyJokes = jokesWithReaction.Where(x => x.ReactionID == funnyReactionID).Count();
            awwJokes = jokesWithReaction.Where(x => x.ReactionID != funnyReactionID).Count();
            rating = ((Convert.ToDouble(funnyJokes) / Convert.ToDouble((funnyJokes + awwJokes))) * 100);
        }


        public async Task<bool> Vote(JokeStats jokeStats)
        {
            var existingJokeStats = await _context.JokeStats
                                                  .FirstOrDefaultAsync(x => x.JokeID == jokeStats.JokeID
                                                                            && x.UserID == jokeStats.UserID);
            if(existingJokeStats != null)
            {
                existingJokeStats.ReactionID = jokeStats.ReactionID;
                _context.JokeStats.Update(existingJokeStats);
            }
            else
            {
                _context.JokeStats.Add(jokeStats);
            }

            if (!await ProcessSave())
                return false;

            var jokerRating = await JokerRating(jokeStats.JokerID);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == jokeStats.JokerID);
            var passingRate = Convert.ToDouble(_context.MasterSettings.FirstOrDefault(x => x.Group == "RatingCategory").Value);
            if (jokerRating >= passingRate)
            {               
                user.isWithBadge = true;
            }else
            {
                user.isWithBadge = false;
            }
            _context.Users.Update(user);

            return await ProcessSave();
        }

        private async Task<bool> ProcessSave()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
