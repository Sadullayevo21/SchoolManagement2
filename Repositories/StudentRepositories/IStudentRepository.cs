using Models.Students;

namespace SchoolManagement2.Repositories.StudentRepositories;

public interface IStudentRepository
{
    void CreateStudent(Student student);
    IEnumerable<Student> GetAllStudents();
    Student GetStudentById(Guid studentId);
    void UpdateStudent(Student student);
    void DeleteStudent(Guid studentId);
}