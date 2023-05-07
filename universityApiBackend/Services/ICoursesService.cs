using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetCoursesByCategory(Category category);
    }
}
