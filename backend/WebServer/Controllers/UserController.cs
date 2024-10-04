using AutoMapper;
using LangLearner.Database.Repositories;
using LangLearner.Exceptions;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;
using LangLearner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace LangLearner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public readonly IUserRepository _userRepository;
        public readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;


        public UserController(IUserRepository userRepository, IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public ActionResult<TokenDto> Register([FromBody] CreateUserDto userDto)
        {
            string token = _userService.Register(userDto);
            return CreatedAtAction(nameof(Register), new TokenDto() { Token=token});
        }

        [HttpPost("login")]
        public ActionResult<TokenDto> Login([FromBody] LoginUserDto userDto)
        {
            string token = _userService.Login(userDto);
            return Ok(new TokenDto() { Token = token });
        }

        [Authorize]
        [HttpGet("stats")]
        public ActionResult<UserStatsDto> GetStats()
        {
            int userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);
            UserStatsDto userStatsDto = _userService.GetStats(userId);
            return Ok(userStatsDto);
        }

    }
}
