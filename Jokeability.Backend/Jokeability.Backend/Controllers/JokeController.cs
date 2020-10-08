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
    public class JokeController : ControllerBase
    {
        private readonly IJokeServices _services;
        private readonly IMapper _mapper;
        public JokeController(IJokeServices services,IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetJokes()
        {
            var jokes = await _services.GetJokes();
            var jokesDTO = _mapper.Map<IEnumerable<JokeDTO>>(jokes);            
            return Ok(jokesDTO);
        }
    }
}