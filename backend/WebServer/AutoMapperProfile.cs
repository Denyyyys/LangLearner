using AutoMapper;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;

namespace LangLearner
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCourseDto, Course>()
                .ForMember(c => c.AvailableLanguages, opt => opt.Ignore());
            CreateMap<Course, CourseDto>()
                .ForMember(cdto => cdto.AvailableLanguages, opt => opt.Ignore());
            CreateMap<CreateUserDto, User>();
            CreateMap<LoginUserDto, User>();
            CreateMap<User, UserStatsDto>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();
            CreateMap<Course, CourseDto>();
        }
    }
}
