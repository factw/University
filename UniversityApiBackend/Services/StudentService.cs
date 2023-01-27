using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentService : IStudentService
    {
        public Task<IEnumerable<Student>> GetStudentsWithCourse()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudentsWithNoCourse()
        {
            throw new NotImplementedException();
        }
    }
}
