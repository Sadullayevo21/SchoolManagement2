using Models.Students;
using SchoolManagement2.Repositories.StudentRepositories;
using System.Data.Common;
using System.Linq;
using SchoolManagement2.Exeptions; 

namespace Services.StudentFile;

public class StudentService : IStudentService
{
    IStudentRepository studentRepository;

    public StudentService()
    {
        studentRepository = new StudentRepository();
    }

    public void CreateStudent(Student student)
    {
        if (student is null)
        {
            throw new ValidationException("Student data cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(student.FirstName))
        {
            throw new ValidationException("First name is required.");
        }

        if (string.IsNullOrWhiteSpace(student.LastName))
        {
            throw new ValidationException("Last name is required.");
        }

        student.Id = Guid.NewGuid();
        var isthere = GetAllStudents().Contains(student);
        if (isthere)
        {
            student.Id = Guid.NewGuid();
        }

        studentRepository.CreateStudent(student);
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return studentRepository.GetAllStudents();
    }

    public void PrintStudent(Student student)
    {
        if (student is null)
        {
            throw new ValidationException("Cannot print null student details.");
        }

        Console.WriteLine("==========================");
        Console.WriteLine(
        $"""
        Student Info:
            First name: {student.FirstName}
            Last name: {student.LastName}
            Adress: {student.Address}
        """
        );
    }

    public bool DeleteStudentById(Guid studentId)
    {
        var student = studentRepository.GetStudentById(studentId);
        if (student is null)
        {
            throw new NotFoundException("Student is not found with the given ID.");
        }
        
        studentRepository.DeleteStudent(studentId);

        return true;
    }

    public Student GetStudentById(Guid studentId)
    {
        var student = studentRepository.GetStudentById(studentId);
        if (student is null)
        {
            throw new NotFoundException("Student is not found.");
        }

        return student;
    }

    public bool UpdateStudent(Student student)
    {
        if (student is null)
        {
            throw new ValidationException("Student updates cannot be null.");
        }

        var updatedstudent = studentRepository.GetStudentById(student.Id);
        if (updatedstudent is null)
        {
            throw new NotFoundException("Student to update is not found.");
        }

        studentRepository.UpdateStudent(student);

        return true;
    }

    public IEnumerable<Student> GetStudentByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException("Search name cannot be empty.");
        }

        var students = GetAllStudents();
        return students.Where(student => student.FirstName == name);
    }

    public int GetStudentsCount()
    {
        var students = GetAllStudents();
        return students.Count();
    }

    public void AddStudentRange(params Student[] students)
    {
        if (students is null || students.Length == 0)
        {
            throw new ValidationException("Student list cannot be empty.");
        }

        foreach(var student in students)
        {
            CreateStudent(student);
        }
    }

    public IEnumerable<Student> GetPaginatedStudents(int page, int pageSize)
    {
        if (page <= 0)
        {
            throw new ValidationException("Page number must be greater than zero.");
        }

        if (pageSize <= 0)
        {
            throw new ValidationException("Page size must be greater than zero.");
        }

        var students = GetAllStudents();
        return students.Skip((page - 1) * pageSize).Take(pageSize);
    }
}