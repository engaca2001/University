using University_Api_Backend.Models.DataModels;

namespace University_Api_Backend.Services
{
    public class StudentService : IStudentService
    {
        public IEnumerable<Course> GetCourses(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsWithCourses()
        {
            // TODO: resolve methods

            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            throw new NotImplementedException();
        }
    }
}
