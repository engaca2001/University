using University_Api_Backend.Models.DataModels;

namespace University_Api_Backend.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetCourseWithNoChapter();
        Chapter GetChapter(int id);

        IEnumerable<Student> GetStudents(int id);
    }
}
