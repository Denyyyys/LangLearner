using AutoMapper;
using LangLearner.Database.Repositories;
using LangLearner.Models.Auth;
using LangLearner.Models.Dtos;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace LangLearner.Services
{
    public interface IUserService
    {
        public string? Register(CreateUserDto userDto);
        public string? Login(LoginUserDto userDto);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IMapper mapper, IIdentityService identityService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _identityService = identityService;
        }

        public string? Login(LoginUserDto userDto)
        {
            var user = _userRepository.GetUserByEmail(userDto.Email);
            if (user == null) return null;
            var passwordVerifyResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);
            if (passwordVerifyResult == PasswordVerificationResult.Success)
            { 
                //var tokenClaims = new TokenClaims() { Email = user.Email, UserId = user.Id };
                string? token = _identityService.GenerateToken(tokenClaims);
                return token;
            }
            return null;

        }

        public string? Register(CreateUserDto userDto)
        {
            User newUser = _mapper.Map<User>(userDto);

            string hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

            newUser.HashedPassword = hashedPassword;

            newUser = _userRepository.AddUser(newUser);
            var tokenClaims = new TokenClaims() { Email = newUser.Email, UserId = newUser.Id };
            string? token = _identityService.GenerateToken(tokenClaims);

            return token;
        }
    }
}
