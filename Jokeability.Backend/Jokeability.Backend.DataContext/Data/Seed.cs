using Jokeability.Backend.DataContext.Models;
using Jokeability.Backend.DataContext.Services.Auth;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.DataContext.Data
{
    public class Seed
    {
        private readonly JokeabilityDBContext _context;
        private readonly IAuthServices _services;
        public Seed(JokeabilityDBContext context, IAuthServices services)
        {
            _context = context;
            _services = services;
        }

        public async Task SeedSetup()
        {
            var cntSetting = countSetting();
            if(cntSetting!=listOfSeedingMasterSetting().Count)
            {
                if(cntSetting>0)
                    await truncateSettings();


                _context.MasterSettings.AddRange(listOfSeedingMasterSetting());
                await _context.SaveChangesAsync();
            }

            if (jokersCount() == 0)
                await FirstJoker();

            if (jokeCount() == 0)
                await CreateInitialJokes();


               
        }

        private int countSetting()
        {
            return _context.MasterSettings.Count();
        }

        private List<MasterSetting> listOfSeedingMasterSetting()
        {
            var masterSettings = new List<MasterSetting>()
            {
                new MasterSetting(){Value="Funny",Description="Funny Vote of Users",Group="VoteCategory",isActive=true},
                new MasterSetting(){Value="Aww",Description="Aww Vote of Users",Group="VoteCategory",isActive=true},
                new MasterSetting(){Value="85",Description="Jokeability Passing Rating",Group="RatingCategory",isActive=true},
            };

            return masterSettings;
        }

        private async Task truncateSettings()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [GS].[tblMasterSetting]";
                    var result = await command.ExecuteNonQueryAsync();
                }
            }
        }

        private int jokersCount()
        {
            return _context.Users.Count();
        }

        private async Task FirstJoker()
        {
            var user = new User()
            {
                FirstName="The First",
                LastName="Joker",
                Username="admin"                
            };
            await _services.Register(user, "admin");            
        }

        private int jokeCount()
        {
            return _context.Jokes.Count();
        }

        private async Task CreateInitialJokes()
        {
            var firstJoker = _context.Users.FirstOrDefault(x => x.Username == "admin");
            var Jokes = new List<Joke>()
            {
                new Joke(){Title="THE SAME JOKES",
                          Description ="My father liked to say, " +
                                       "“I’m bald because a good man always comes out on top.” " +
                                       "Dad loved to make people laugh. At his funeral, the preacher said, " +
                                       "“In his lifetime, this man told thousands of jokes, " +
                                       "but they were always the same one.”",
                          JokerID=firstJoker.Id,
                          isActive=true},
                new Joke(){Title="SCREW MY CAP ON",
                        Description ="If it was a blustery day, you could be sure to hear my dad remark, " +
                                    "“It was so windy today, I had to wrinkle my forehead and screw " +
                                    "my cap on to keep it there!”",
                        JokerID=firstJoker.Id,
                        isActive=true},
               new Joke(){Title="COUNTED THEIR LEGS",
                          Description ="As my sister and I were counting the cows in a pasture, Dad glanced over " +
                                       "at the herd and said, “There are 127.” “How’d you know?” we asked. " +
                                       "He replied, “I counted their legs and divided by four.” Decades later, " +
                                       "my kids give me the same look I gave my dad every time I pull that same gag.",
                          JokerID=firstJoker.Id,
                          isActive=true},
                new Joke(){Title="SIRS AND MAMAS",
                        Description ="My granddaughter's husband was complaining about how spellcheck " +
                                     "changes the meaning of e-mails when an Air Force officer told him this story: " +
                                     "He’d sent a message to 300 of his personnel addressed to “Dear Sirs and Ma’ams.” " +
                                     "It was received as “Dear Sirs and Mamas.",
                        JokerID=firstJoker.Id,
                        isActive=true}
            };
            await _context.Jokes.AddRangeAsync(Jokes);
            await _context.SaveChangesAsync();
        }
    }
}
