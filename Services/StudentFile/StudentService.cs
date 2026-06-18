using Models.Students;
using SchoolManagement2.Repositories.StudentRepositories;
using System.Data.Common;
using System.Linq;

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
        var student = GetStudentById(studentId);
        if (student is null)
        {
            return false;
        }
        
        studentRepository.DeleteStudent(studentId);

        return true;
    }

    public Student GetStudentById(Guid studentId)
    {
        return studentRepository.GetStudentById(studentId);
    }

    public bool UpdateStudent(Student student)
    {
        var updatedstudent = GetStudentById(student.Id);
        if (updatedstudent is null)
        {
            return false;
        }

        studentRepository.UpdateStudent(student);

        return true;
    }

    public IEnumerable<Student> GetStudentByName(string name)
    {
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
        foreach(var student in students)
        {
            CreateStudent(student);
        }
    }

    public IEnumerable<Student> GetPaginatedStudents(int page, int pageSize)
    {
        var students = GetAllStudents();
        return students.Skip((page - 1) * pageSize).Take(pageSize);
    }
}