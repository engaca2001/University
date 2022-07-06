using University_Api_Backend.Models.DataModels;

namespace University_Api_Backend.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();

        IEnumerable<Course> GetCourses(int Id);
    }
}
