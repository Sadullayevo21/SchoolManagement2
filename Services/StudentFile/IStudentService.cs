using Models.Students;

namespace Services.StudentFile;

public interface IStudentService
{
    void CreateStudent(Student student);
    Dictionary<int, Student> GetAllStudents();
    void PrintStudent(Student student);
    Student GetStudentById(Guid studentId);
    void UpdateStudent(Student student);
    void DeleteStudentById(Guid studentId);
    IEnumerable<KeyValuePair<int, Student>> GetStudentByName(string name);
    int GetStudentsCount();
    void AddStudentRange(params Student[] students);
    IEnumerable<KeyValuePair<int, Student>> GetPaginatedStudents(int page, int pageSize);
}