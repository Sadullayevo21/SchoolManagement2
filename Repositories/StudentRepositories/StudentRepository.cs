using System.Text.Json;
using Models.Students;
using System.Linq;

namespace SchoolManagement2.Repositories.StudentRepositories;

public class StudentRepository : IStudentRepository
{
    string path;
    public StudentRepository()
    {
        path = "students.json";
    }
   
    public void CreateStudent(Student student)
    {
        
        List<Student> students = GetAllStudents().ToList();
        students.Add(student);
        var data = JsonSerializer.Serialize(students);
        File.WriteAllText(path, data);
    }

    public void DeleteStudent(Guid studentId)
    { 
        var student = GetAllStudents().ToList();
        var deletestudent = student.FirstOrDefault(eachstudent => eachstudent.Id == studentId);
        student.Remove(deletestudent);
        var studentdata = JsonSerializer.Serialize(student);
        File.WriteAllText(path, studentdata);
    }

    public IEnumerable<Student> GetAllStudents()
    {
        var data = File.ReadAllText(path);
        return JsonSerializer.Deserialize<IEnumerable<Student>>(data);
    }

    public Student GetStudentById(Guid studentId)
    {
        var students = GetAllStudents();
        return students.FirstOrDefault(student => student.Id == studentId);
    }
   
    public void UpdateStudent(Student student)
    {
        var students = GetAllStudents().ToList();
        var deletedstudent = students.FirstOrDefault(eachstudent => eachstudent.Id == student.Id);
        students.Remove(deletedstudent);
        students.Add(student);
        var data = JsonSerializer.Serialize(students);
        File.WriteAllText(path, data);
    }
}