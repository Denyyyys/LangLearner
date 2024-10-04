using AutoFixture;
using AutoMapper;
using LangLearner;
using LangLearner.Controllers;
using LangLearner.Database.Repositories;
using LangLearner.Exceptions;
using LangLearner.Models.Auth;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;
using LangLearner.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBackend.ServicesTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILanguageRepository> _languageRepositoryMock;

        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
        private readonly IUserService _userService;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _userRepositoryMock = new Mock<IUserRepository>();
            _languageRepositoryMock = new Mock<ILanguageRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
            _identityServiceMock = new Mock<IIdentityService>();
            _userService = new UserService(_userRepositoryMock.Object, _languageRepositoryMock.Object, _passwordHasherMock.Object, _mapper, _identityServiceMock.Object);
        }


        [Fact]
        public void RegisterCorrectUserWithoutAppLanguage()
        {
            // Arrange
            string email = "testUser@mail.com";
            string username = "testUserName";
            string tokenMock = "someToken";
            string nativeLanguage = "english";
            string password = "password";
            var correctLanguage = new Language() { };
            var newUserDto = new CreateUserDto()
            { Email = email, NativeLanguageName = nativeLanguage, Password = password, Username = username };
            var newUserCreated = new User
            {
                Email = email,
                NativeLanguageName = nativeLanguage,
                HashedPassword = "somehash",
                UserName = username,
                AppLanguageName = nativeLanguage,
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).Returns(newUserCreated);
            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(It.IsAny<string>())).Returns(correctLanguage);
            _identityServiceMock.Setup(service => service.GenerateToken(It.IsAny<TokenClaims>())).Returns(tokenMock);

            // Act
            var token = _userService.Register(newUserDto);

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByEmail(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByUserName(username), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddUser(It.Is<User>(user =>
                user.Email == email &&
                user.NativeLanguageName == nativeLanguage &&
                user.UserName == username &&
                user.AppLanguageName == nativeLanguage
            )), Times.Once);
            _identityServiceMock.Verify(service => service.GenerateToken(It.IsAny<TokenClaims>()), Times.Once);

            Assert.Equal(tokenMock, token);
        }

        [Fact]
        public void RegisterCorrectUserWithAppLanguage()
        {
            // Arrange
            string email = "testUser@mail.com";
            string username = "testUserName";
            string tokenMock = "someToken";
            string nativeLanguage = "english";
            string appLanguage = "spanish";
            string password = "password";
            var correctLanguage = new Language() { };
            var newUserDto = new CreateUserDto()
            { Email = email, NativeLanguageName = nativeLanguage, AppLanguageName=appLanguage, Password = password, Username = username };
            var newUserCreated = new User
            {
                Email = email,
                NativeLanguageName = nativeLanguage,
                HashedPassword = "somehash",
                UserName = username,
                AppLanguageName = nativeLanguage,
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).Returns(newUserCreated);
            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(It.IsAny<string>())).Returns(correctLanguage);

            _identityServiceMock.Setup(service => service.GenerateToken(It.IsAny<TokenClaims>())).Returns(tokenMock);

            // Act
            var token = _userService.Register(newUserDto);

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByEmail(email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByUserName(username), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddUser(It.Is<User>(user =>
                user.Email == email &&
                user.NativeLanguageName == nativeLanguage &&
                user.UserName == username &&
                user.AppLanguageName == appLanguage
            )), Times.Once);
            _identityServiceMock.Verify(service => service.GenerateToken(It.IsAny<TokenClaims>()), Times.Once);

            Assert.Equal(tokenMock, token);
        }

        [Theory]
        [InlineData("english", "dummylanguage")]
        [InlineData("dummylanguage", "english")]
        public void RegisterLanguageDoesNotExists(string appLanguage, string nativeLanguage)
        {
            // Arrange
            string email = "testUser@mail.com";
            string username = "testUserName";
            string password = "password";
            var correctLanguage = new Language() { };
            var newUserDto = new CreateUserDto()
            { Email = email, NativeLanguageName = nativeLanguage, AppLanguageName = appLanguage, Password = password, Username = username };
            var newUserCreated = new User
            {
                Email = email,
                NativeLanguageName = nativeLanguage,
                HashedPassword = "somehash",
                UserName = username,
                AppLanguageName = nativeLanguage,
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).Returns(newUserCreated);

            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(It.Is<string>(name => name == "english"))).Returns(correctLanguage);
            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(It.Is<string>(name => name != "english"))).Returns((Language?)null);


            // Act and Assert
            var exception = Assert.Throws<GeneralAPIException>(() => _userService.Register(newUserDto));
            Assert.Equal("Provided application or native language is not supported", exception.Message);
            Assert.Equal(400, exception.StatusCode);
        }

        [Fact]
        public void RegisterUserWithProvidedUserNameAlreadyExists()
        {
            // Arrange
            string email = "notUnique@mail.com";
            string username = "notUniqueUsername";
            string nativeLanguage = "english";
            string appLanguage = "spanish";
            string password = "password";

            var newUserDto = new CreateUserDto()
            { Email = email, Password=password, NativeLanguageName = nativeLanguage, AppLanguageName = appLanguage, Username = username };
            var newUserCreated = new User
            {
                Email = email,
                NativeLanguageName = nativeLanguage,
                HashedPassword = "somehash",
                UserName = username,
                AppLanguageName = nativeLanguage,
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns(new User() { });
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns(new User() { });
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).Returns(newUserCreated);

            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(It.IsAny<string>())).Returns(new Language() { });


            // Act and Assert
            var exception = Assert.Throws<GeneralAPIException>(() => _userService.Register(newUserDto));
            Assert.Equal("User with provided email already exists", exception.Message);
            Assert.Equal(409, exception.StatusCode);
        }

        [Fact]
        public void RegisterUserWithProvidedEmailAlreadyExists()
        {
            // Arrange
            string email = "testUser@mail.com";
            string username = "userName";
            string nativeLanguage = "english";
            string appLanguage = "spanish";
            string password = "password";

            var newUserDto = new CreateUserDto()
            { Email = email, Password = password, NativeLanguageName = nativeLanguage, AppLanguageName = appLanguage, Username = username };
            var newUserCreated = new User
            {
                Email = email,
                NativeLanguageName = nativeLanguage,
                HashedPassword = "somehash",
                UserName = username,
                AppLanguageName = nativeLanguage,
            };



            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns(new User() { });
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns((User?)null);
            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).Returns(newUserCreated);

            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(It.IsAny<string>())).Returns(new Language() { });


            // Act and Assert
            var exception = Assert.Throws<GeneralAPIException>(() => _userService.Register(newUserDto));
            Assert.Equal("User with provided email already exists", exception.Message);
            Assert.Equal(409, exception.StatusCode);
        }

        [Fact]
        public void LoginUserInvalidPassword()
        {
            string email = "testUser@mail.com";
            string notValidpassword = "notPassword";
            string hashedPassword = "hashedPassword";
            User user = new User() { Email = email, HashedPassword =  hashedPassword};
            LoginUserDto loginUserDto = new LoginUserDto() { Email = email, Password = notValidpassword };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns(user);
            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user, hashedPassword, notValidpassword)).Returns(PasswordVerificationResult.Failed);
            var exception = Assert.Throws<GeneralAPIException>(() => _userService.Login(loginUserDto));
            Assert.Equal("Bad credentials provided!", exception.Message);
            Assert.Equal(401, exception.StatusCode);    


        }

        [Fact]
        public void LoginUserEmailNotExists()
        {
            string badEmail = "badEmail@mail.com";
            string notValidpassword = "notPassword";
            
            LoginUserDto loginUserDto = new LoginUserDto() { Email = badEmail, Password = notValidpassword };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(badEmail)).Returns((User?)null);
            var exception = Assert.Throws<GeneralAPIException>(() => _userService.Login(loginUserDto));
            
            Assert.Equal("Bad credentials provided!", exception.Message);
            Assert.Equal(401, exception.StatusCode);
        }

        [Fact]
        public void LoginUserSuccess()
        {
            string email = "email@mail.com";
            string password = "password";
            string hashedPassword = "hashedPassword";
            string generatedToken = "si821@C#C@#!@#E";            
            LoginUserDto loginUserDto = new LoginUserDto() { Email = email, Password = password };
            User user = new User() { Email= email, HashedPassword= hashedPassword};
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns(user);
            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user, hashedPassword, password)).Returns(PasswordVerificationResult.Success);
            _identityServiceMock.Setup(service => service.GenerateToken(It.IsAny<TokenClaims>()))
                               .Returns(generatedToken);

            string resultToken = _userService.Login(loginUserDto);
            Assert.Equal(generatedToken, resultToken);
        }

    }
}
