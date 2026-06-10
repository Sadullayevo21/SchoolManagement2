using Models.Students;
using System.Linq;

namespace Services.StudentFile;

public class StudentService : IStudentService
{
    private Dictionary<int, Student> students;  

    public StudentService()
    {
        students = new Dictionary<int, Student>()
        {
            {
                1, new Student
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Omadjon",
                    LastName = "Ismoilov",
                    Address = "Toshkent"
                }
            },
            {
                2, new Student
                {
                    Id = Guid.Parse("3dc2988f-4345-4266-88d0-36f9bc121ff0"),
                    FirstName = "Akbar",
                    LastName = "Aliyev",
                    Address = "Toshkent"
                }
            },
            {
                3, new Student
                {
                    Id = Guid.Parse("a4437ee1-2703-435d-a0ad-ba69c537b6b2"),
                    FirstName = "Ahmad",
                    LastName = "Inomov",
                    Address = "Toshkent"
                }
            }
        };
    }
    
    private int IndexOfDictionary = 4;
    
    public void CreateStudent(Student student)
    {
       students.Add(IndexOfDictionary, student);
       IndexOfDictionary++;
    }

    public Dictionary<int, Student> GetAllStudents()
    {
        return this.students;
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
    
    public void DeleteStudentById(Guid studentId)
    {
        foreach(var student in students)
        {
            if(student.Value.Id == studentId)
            {
                students.Remove(student.Key);
                return;
            }
        }
    }

    public Student GetStudentById(Guid studentId)
    {
        foreach(var student in students)
        {
            if(student.Value.Id == studentId)
            {
                return student.Value;
            }
        }

        return null;
    }

    public void UpdateStudent(Student student)
    {
        foreach(var newStudent in students)
        {
            if(newStudent.Value.Id == student.Id)
            {
                students.Remove(newStudent.Key);
                students.Add(newStudent.Key, student);
                return;
            }
        }
    }

    public IEnumerable<KeyValuePair<int, Student>> GetStudentByName(string name)
    {
        return students.Where(student => student.Value.FirstName == name);
    }

    public int GetStudentsCount()
    {
        return students.Count();
    }

    public void AddStudentRange(params Student[] students)
    {
        foreach(var student in students)
        {
            CreateStudent(student);
        }
    }

    public IEnumerable<KeyValuePair<int, Student>> GetPaginatedStudents(int page, int pageSize)
    {
        return students.Skip((page -1) * pageSize).Take(pageSize);
    }
}