using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Jokeability.Backend.DataContext.Models;
using Jokeability.Backend.DataContext.Services.Auth;
using Jokeability.Backend.DTO;
using Jokeability.Backend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jokeability.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthController(IAuthServices authServices, IMapper mapper, IConfiguration config)
        {
            _authService = authServices;
            _mapper = mapper;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            if (await _authService.isExistUser(userRegisterDTO.Username.ToLower()))
                return BadRequest("User already exists in the database");

            var newUser = new User();
            newUser = _mapper.Map<User>(userRegisterDTO);
            var createdUser = await _authService.Register(newUser, userRegisterDTO.Password);
            if (createdUser == null)
                return BadRequest("Unable to create user");

            var userDTO = new UserDTO();
            userDTO = _mapper.Map<UserDTO>(createdUser);
            userDTO.Token = GetAuthenticatedToken(createdUser.Username, createdUser.Id.ToString());
            userDTO.Expiry = ComputationHelper.GetTokenValidityInSeconds(1);
            return Ok(userDTO);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var userLogin = await _authService.Login(userLoginDTO.Username.ToLower(), userLoginDTO.Password);
            if (userLogin == null)
                return BadRequest("Incorrect user email or password. Please make sure you input the correct details");

            var userDTO = new UserDTO();
            userDTO = _mapper.Map<UserDTO>(userLogin);
            userDTO.Token = GetAuthenticatedToken(userLogin.Username, userLogin.Id.ToString());
            userDTO.Expiry = ComputationHelper.GetTokenValidityInSeconds(1);
            return Ok(userDTO);
        }


        private string GetAuthenticatedToken(string email, string id)
        {
            var claims = new[]
         {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Name,email)
            };

            var keys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:secretkey").Value));
            var creds = new SigningCredentials(keys, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var returnToken = tokenHandler.WriteToken(token);
            return returnToken;
        }
    }
}