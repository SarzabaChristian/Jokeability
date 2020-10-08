using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Jokeability.Backend.DataContext.Models;
using Jokeability.Backend.DataContext.Services;
using Jokeability.Backend.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jokeability.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJokeServices _services;
        private readonly IMapper _mapper;
        public UserController(IJokeServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }


        [HttpPost("AddJoke")]
        public async Task<IActionResult> Add(NewJokeDTO newJokeDTO)
        {
            var newJoke = _mapper.Map<Joke>(newJokeDTO);
            newJoke.isActive = true;
            var savedJoke = await _services.Add(newJoke);
            if (savedJoke == null)
                return BadRequest("Unable to save the joke");

            var joke = _mapper.Map<JokeDTO>(savedJoke);
            return Ok(joke);
        }
        [HttpPost("Vote")]
        public async Task<IActionResult> Vote(JokeStatsDTO jokeStatsDTO)
        {
            var reactionID = await _services.GetReactionID(jokeStatsDTO.Reaction);
            if (reactionID == 0)
                return BadRequest("Invalid voting reaction");

            var newReaction = _mapper.Map<JokeStats>(jokeStatsDTO);
            newReaction.ReactionID = reactionID;
            newReaction.isActive = true;
            if (!await _services.Vote(newReaction))
                return BadRequest("Cannot save the data");

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJoker(int id)
        {
            var joker = await _services.GetJoker(id);
            var jokerDTO = _mapper.Map<JokerDTO>(joker);
            int totalJokes,funnyJokes, awwJokes;
            double rating;
            _services.GetRatings(id,out totalJokes, out funnyJokes, out awwJokes, out rating);
            jokerDTO.FunnyJokes = funnyJokes;
            jokerDTO.AwwJokes = awwJokes;
            jokerDTO.Rating = rating > 0 ? Math.Round(rating,2):0;
            jokerDTO.TotalJokes = totalJokes;
            if (jokerDTO == null)
                return BadRequest("Joker does not exists");
            
            return Ok(jokerDTO);
        }
    }
}