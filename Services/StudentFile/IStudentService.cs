using Models.Students;

namespace Services.StudentFile;

public interface IStudentService
{
    void CreateStudent(Student student);
    IEnumerable<Student> GetAllStudents();
    void PrintStudent(Student student);
    Student GetStudentById(Guid studentId);
    bool UpdateStudent(Student student);
    bool DeleteStudentById(Guid studentId);
    IEnumerable<Student> GetStudentByName(string name);
    int GetStudentsCount();
    void AddStudentRange(params Student[] students);
    IEnumerable<Student> GetPaginatedStudents(int page, int pageSize);
}