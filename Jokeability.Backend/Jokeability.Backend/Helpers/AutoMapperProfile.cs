using AutoMapper;
using Jokeability.Backend.DataContext.Models;
using Jokeability.Backend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRegisterDTO>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<NewJokeDTO, Joke>();
            CreateMap<Joke, JokeDTO>()
                    .ForMember(dest=>dest.JokerName, src=> {
                        src.ResolveUsing(x => x.Joker.FullName);
                    })
                    .ForMember(dest=>dest.withBadge, src=> {
                        src.ResolveUsing(x => x.Joker.isWithBadge);
                    });

            CreateMap<JokeStatsDTO, JokeStats>();

            CreateMap<User, JokerDTO>();
        }
    }
}
