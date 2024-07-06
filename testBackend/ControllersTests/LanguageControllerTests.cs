using LangLearner.Controllers;
using LangLearner.Services;
using Microsoft.Extensions.Logging;
using LangLearner.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using LangLearner.Database.Repositories;
using AutoFixture;
using LangLearner.Models.Dtos.Responses;
using AutoMapper;
using LangLearner;

namespace testBackend.controllersTests
{
    public class LanguageControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ILanguageRepository> _languageRepositoryMock;
        private readonly ILanguagesService _languagesService;
        private readonly Mock<ILogger<LanguageController>> _loggerMock;
        //private readonly Mock<IMapper> _mapperMock;
        private readonly IMapper _mapper;



        public LanguageControllerTests()
        {
            
            _fixture = new Fixture();
            _fixture.Customize<Language>(composer =>
                composer
                    .Without(l => l.NativeLanguageUsers)
                    .Without(l => l.AppLanguageUsers)
            );
            _languageRepositoryMock = new Mock<ILanguageRepository>();
            _languagesService = new LanguagesService(_languageRepositoryMock.Object);
            _loggerMock = new Mock<ILogger<LanguageController>>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }


        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void GetAllNotEmpty(int numberLanguages)
        {
            // Assign
            var languageList = _fixture.CreateMany<Language>(numberLanguages).ToList();
            _languageRepositoryMock.Setup(repo => repo.GetAll()).Returns(languageList);
            var _controller = new LanguageController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object, _mapper);

            // Act
            OkObjectResult? result = _controller.GetAll().Result as OkObjectResult;
            IEnumerable<LanguageDto>? languages = result?.Value as IEnumerable<LanguageDto>;

            // Assert
            Assert.Equal(300, result?.StatusCode);
            Assert.Equal(numberLanguages, languages?.Count());
        }


        [Fact]
        public void GetAllEmpty()
        {
            // Assign
            var languageList = _fixture.CreateMany<Language>(0).ToList();
            _languageRepositoryMock.Setup(repo => repo.GetAll()).Returns(languageList);
            var _controller = new LanguageController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object, _mapper);

            // Act
            NotFoundObjectResult? result = _controller?.GetAll().Result as NotFoundObjectResult;
            var errorResponse = result?.Value as ApiError;

            // Assert
            Assert.Equal(404, result?.StatusCode);
            Assert.Equal("No language found!", errorResponse?.ErrorMessage);
            Assert.Equal(404, errorResponse?.StatusCode);

        }


        [Fact]
        public void GetByAnyExists()
        {
            // Assign
            var code = "en";
            var language = _fixture.Create<Language>();
            language.Code = code;
            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(code)).Returns(language);
            var _controller = new LanguageController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object, _mapper);

            // Act
            OkObjectResult? result = _controller?.GetByAny(code).Result as OkObjectResult;

            // Assert
            var languageFromAPI = result?.Value as Language;
            Assert.Equal(200, result?.StatusCode);
            Assert.Equal(code, languageFromAPI?.Code);
        }

        [Fact]
        public void GetByAnyNotExists()
        {
            // Assign
            var dummyCode = "ens";
            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(dummyCode)).Returns((Language?)null);
            var _controller = new LanguageController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object, _mapper);

            // Act
            NotFoundObjectResult? result = _controller?.GetByAny(dummyCode).Result as NotFoundObjectResult;
            
            // Assert
            ApiError? languageFromAPI = result?.Value as ApiError;
            Assert.Equal(404, result?.StatusCode);
            Assert.Equal($"No language with field value: `{dummyCode}` found! Please, check your data", languageFromAPI?.ErrorMessage);
            Assert.Equal(404, languageFromAPI?.StatusCode);
        }
    }
}
