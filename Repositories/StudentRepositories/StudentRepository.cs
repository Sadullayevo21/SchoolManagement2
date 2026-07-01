using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Models.Students;

namespace SchoolManagement2.Repositories.StudentRepositories;

public class StudentRepository : IStudentRepository
{
    string path;
    public StudentRepository()
    {
        path = "students.json";
    }
   
    public async Task CreateStudentAsync(Student student)
    {
        List<Student> students = (await GetAllStudentsAsync()).ToList();
        students.Add(student);
        var data = JsonSerializer.Serialize(students);
        await File.WriteAllTextAsync(path, data);
    }

    public async Task DeleteStudentAsync(Guid studentId)
    { 
        var student = (await GetAllStudentsAsync()).ToList();
        var deletestudent = student.FirstOrDefault(eachstudent => eachstudent.Id == studentId);
        if (deletestudent != null)
        {
            student.Remove(deletestudent);
            var studentdata = JsonSerializer.Serialize(student);
            await File.WriteAllTextAsync(path, studentdata);
        }
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        if (!File.Exists(path)) return new List<Student>();
        var data = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<IEnumerable<Student>>(data) ?? new List<Student>();
    }

    public async Task<Student?> GetStudentByIdAsync(Guid studentId)
    {
        var students = await GetAllStudentsAsync();
        return students.FirstOrDefault(student => student.Id == studentId);
    }
   
    public async Task UpdateStudentAsync(Student student)
    {
        var students = (await GetAllStudentsAsync()).ToList();
        var deletedstudent = students.FirstOrDefault(eachstudent => eachstudent.Id == student.Id);
        if (deletedstudent != null)
        {
            students.Remove(deletedstudent);
        }
        students.Add(student);
        var data = JsonSerializer.Serialize(students);
        await File.WriteAllTextAsync(path, data);
    }
}