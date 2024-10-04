using LangLearner.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LangLearner.Database.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses(string sortOrder, int pageNumber, int pageSize);
        IEnumerable<Course> GetAllCoursesByCreator(int id);
        IEnumerable<Course> GetAllUserCourses(int id);

        Course GetCourse(int id);

        void AddCourse(Course course);

        IEnumerable<Course> GetCoursesByKeywords(string[] keywords);
    }

    public class CourseRepository : ICourseRepository
    {

        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses(string sortOrder, int pageNumber, int pageSize)
        {
            int skipNumber = (pageNumber - 1) * pageSize;

            return _context.Courses.Skip(skipNumber ).Take(pageSize).Include(c => c.AvailableLanguages).Include(c => c.EnrolledUsers).Include(c => c.Creator).AsEnumerable();
        }

        public IEnumerable<Course> GetAllCoursesByCreator(int id)
        {
            return _context.Courses.Where(c => c.CreatorId == id).AsEnumerable();
        }

        public IEnumerable<Course> GetAllUserCourses(int id)
        {
            throw new Exception();
        }

        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCoursesByKeywords(string[] keywords)
        {
            throw new NotImplementedException();
        }
    }
}
