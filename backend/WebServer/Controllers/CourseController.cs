using AutoMapper;
using LangLearner.Database.Repositories;
using LangLearner.Exceptions;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;
using LangLearner.Services;
using LangLearner.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace LangLearner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        public readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public ActionResult<List<CourseDto>> GetAll([FromQuery] string sortOrder = "date", [FromQuery] int pageNumber = 1, [FromQuery] int maxPageSize = APIConstants.CourseMaxPageSize)
        {
            List<CourseDto> courses = _courseService.GetAllAsDto(sortOrder, pageNumber, maxPageSize);
            return Ok(courses);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Course> Create([FromBody] CreateCourseDto createCourseDto)
        {            
            int userId = int.Parse(User.FindFirst("UserId")!.Value);
            Course newCourse = _courseService.Create(createCourseDto, userId);
            return Ok(newCourse);
        }

    }
}
