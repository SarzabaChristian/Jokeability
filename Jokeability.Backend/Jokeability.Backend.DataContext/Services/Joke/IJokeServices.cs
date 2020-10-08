using Jokeability.Backend.DataContext.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Jokeability.Backend.DataContext.Services
{
    public interface IJokeServices
    {
        Task<Joke> Add(Joke newJoke);
        Task<bool> Vote(JokeStats jokeStats);
        Task<List<Joke>> GetJokes();
        Task<int> GetReactionID(string reaction);
        Task<double> JokerRating(int jokerId);
        Task<User> GetJoker(int id);
        void GetRatings(int jokerId, out int totalJokes,out int funnyJokes, out int awwJokes, out double rating);
    }
}
