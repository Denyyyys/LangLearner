using AutoMapper;
using LangLearner.Database.Repositories;
using LangLearner.Exceptions;
using LangLearner.Models.Auth;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LangLearner.Services
{
    public interface ICourseService
    {
        Course Create(CreateCourseDto userDto, int creatorId);
        List<CourseDto> GetAllAsDto(string sortOrder, int pageNumber, int pageSize);
    }
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly ILanguageRepository _languageRepository;

        public CourseService(IMapper mapper, ICourseRepository courseRepository, ILanguageRepository languageRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
            _languageRepository = languageRepository;
        }

        public Course Create(CreateCourseDto userDto, int creatorId)
        {
            Course newCourse = new Course();
            newCourse.CreatorId = creatorId;
            foreach (var language in userDto.AvailableLanguages)
            {
                Language? languageFromDb = _languageRepository.GetLanguageByAny(language);
                if (languageFromDb is null)
                    throw new BadRequestException($"Provided available language: {language} is inappropriate or not supported");

                newCourse.AvailableLanguages.Add(languageFromDb);
            }
            Language? targetLanguageNameFromDb = _languageRepository.GetLanguageByAny(userDto.TargetLanguageName);
            if (targetLanguageNameFromDb is null)
                throw new BadRequestException($"Provided target language name: {userDto.TargetLanguageName} is inappropriate or not supported");

            userDto.TargetLanguageName = targetLanguageNameFromDb.Name;
            _mapper.Map(userDto, newCourse);

            _courseRepository.AddCourse(newCourse);
            return newCourse;
        }

        public List<CourseDto> GetAllAsDto(string sortOrder, int pageNumber, int pageSize)
        {
            List<Course> courses = _courseRepository.GetAllCourses(sortOrder, pageNumber, pageSize).ToList();
            List<CourseDto> coursesDtos = _mapper.Map<List<CourseDto>>(courses);
            for (int i = 0; i < courses.Count(); i++)
            {
                coursesDtos[i].numberEnrolledUsers = courses[i].EnrolledUsers.Count();
                coursesDtos[i].CreatorUsername = courses[i].Creator.UserName;
                coursesDtos[i].AvailableLanguages = courses[i].AvailableLanguages.Select(lang => lang.Name).ToList();
            }
            return coursesDtos;
        }
    }
}
