using Jokeability.Backend.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jokeability.Backend.DataContext.Services.Auth
{
    public interface IAuthServices
    {
        Task<bool> isExistUser(string username);
        Task<User> Register(User newUser, string password);
        Task<User> Login(string username, string password);
    }
}
