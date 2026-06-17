using Models.Students;
using SchoolManagement2.Repositories.StudentRepositories;

namespace Services.StudentFile;

public class StudentService : IStudentService
{
    private readonly StudentRepository _studentRepository;

    public StudentService()
    {
        _studentRepository = new StudentRepository();

        if (!_studentRepository.GetAll().Any())
        {
            _studentRepository.Create(new Student { Id = Guid.NewGuid(), FirstName = "Omadjon", LastName = "Ismoilov", Address = "Toshkent", Grade = 4, Age = 20 });
            _studentRepository.Create(new Student { Id = Guid.Parse("3dc2988f-4345-4266-88d0-36f9bc121ff0"), FirstName = "Akbar", LastName = "Aliyev", Address = "Toshkent", Grade = 5, Age = 18 });
            _studentRepository.Create(new Student { Id = Guid.Parse("a4437ee1-2703-435d-a0ad-ba69c537b6b2"), FirstName = "Ahmad", LastName = "Inomov", Address = "Toshkent", Grade = 3, Age = 22 });
        }
    }

    public void CreateStudent(Student student)
    {
        _studentRepository.Create(student);
    }

    public Dictionary<int, Student> GetAllStudents()
    {
        int index = 1;
        return _studentRepository.GetAll().ToDictionary(s => index++, s => s);
    }

    public Student GetStudentById(Guid studentId)
    {
        return _studentRepository.GetAll().FirstOrDefault(s => s.Id == studentId);
    }

    public void UpdateStudent(Student student)
    {
        _studentRepository.Update(student => student.Id == student.Id, student);
    }

    public void DeleteStudentById(Guid studentId)
    {
        _studentRepository.Delete(student => student.Id == studentId);
    }

    public void PrintStudent(Student student)
    {
        Console.WriteLine("==========================");
        Console.WriteLine($"Student Info:\n   First name: {student.FirstName}\n   Last name: {student.LastName}\n   Address: {student.Address}");
    }

    public IEnumerable<KeyValuePair<int, Student>> GetStudentByName(string name)
    {
        return GetAllStudents().Where(student => student.Value.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public int GetStudentsCount()
    {
        return _studentRepository.GetAll().Count();
    }

    public void AddStudentRange(params Student[] students)
    {
        foreach (var student in students)
        {
            _studentRepository.Create(student);
        }
    }

    public IEnumerable<KeyValuePair<int, Student>> GetPaginatedStudents(int page, int pageSize)
    {
        return GetAllStudents().Skip((page - 1) * pageSize).Take(pageSize);
    }
}