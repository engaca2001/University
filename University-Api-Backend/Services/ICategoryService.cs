using University_Api_Backend.Models.DataModels;

namespace University_Api_Backend.Services
{
    public interface ICategoryService
    {
        IEnumerable<Course> GetCoursesCategory(Category category);


    }
}
