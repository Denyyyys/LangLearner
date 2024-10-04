using AutoMapper;
using LangLearner.Database.Repositories;
using LangLearner.Exceptions;
using LangLearner.Models.Auth;
using LangLearner.Models.Dtos;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LangLearner.Services
{
    public interface IUserService
    {
        string Register(CreateUserDto userDto);
        string Login(LoginUserDto userDto);

        public UserStatsDto GetStats(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly IIdentityService _identityService;
        private readonly ILanguageRepository _languageRepository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ILanguageRepository languageRepository, IPasswordHasher<User> passwordHasher, IMapper mapper, IIdentityService identityService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _identityService = identityService;
            _languageRepository = languageRepository;
        }

        public UserStatsDto GetStats(int userId)
        {
            User? user = _userRepository.GetUserById(userId) ?? throw new GeneralAPIException("Unexpected error occured. Please try to log in one more time");

            UserStatsDto userStatsDto = _mapper.Map<UserStatsDto>(user);
            return userStatsDto;
        }

        public string Login(LoginUserDto userDto)
        {
            var user = _userRepository.GetUserByEmail(userDto.Email);
            if (user == null)
                throw new GeneralAPIException("Bad credentials provided!") { StatusCode = 401 };

            var passwordVerifyResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);
            if (passwordVerifyResult != PasswordVerificationResult.Success)
                throw new GeneralAPIException("Bad credentials provided!") { StatusCode = 401 };
            
            var tokenClaims = new TokenClaims() { Email = user.Email, UserId = user.Id };
            string token = _identityService.GenerateToken(tokenClaims);
            return token;

        }

        public string Register(CreateUserDto userDto)
        {
            User newUser = _mapper.Map<User>(userDto);

            string hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

            newUser.HashedPassword = hashedPassword;

            if (newUser.AppLanguageName == string.Empty)
                newUser.AppLanguageName = newUser.NativeLanguageName;

            if (_userRepository.GetUserByUserName(newUser.UserName) != null)
                throw new GeneralAPIException("User with provided username already exists, please think about new one :)") { StatusCode = 409 };

            if(_userRepository.GetUserByEmail(newUser.Email) != null)
                throw new GeneralAPIException("User with provided email already exists") { StatusCode = 409 };

            string? newUserAppLanguageName = _languageRepository.GetLanguageByAny(newUser.AppLanguageName)?.Name;
            string? newUserNativeLanguageName = _languageRepository.GetLanguageByAny(newUser.NativeLanguageName)?.Name;
            
            if (newUserAppLanguageName == null || newUserNativeLanguageName == null)
                throw new GeneralAPIException("Provided application or native language is not supported") { StatusCode = 400 };

            newUser = _userRepository.AddUser(newUser);
            var tokenClaims = new TokenClaims() { Email = newUser.Email, UserId = newUser.Id };
            string token = _identityService.GenerateToken(tokenClaims);

            return token;
        }
    }
}
