using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsWithCourse();

        Task<IEnumerable<Student>> GetStudentsWithNoCourse();        
    }
}
 