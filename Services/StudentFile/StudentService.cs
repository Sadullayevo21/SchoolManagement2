using Models.Students;
using SchoolManagement2.Repositories.StudentRepositories;
using System.Data.Common;
using System.Linq;
using SchoolManagement2.Exeptions; 
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Services.StudentFile;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService()
    {
        _studentRepository = new StudentRepository();
    }

    public async Task CreateStudentAsync(Student student)
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
        
        var allStudents = await GetAllStudentsAsync();
        var isThere = allStudents.Contains(student);
        if (isThere)
        {
            student.Id = Guid.NewGuid();
        }

        await _studentRepository.CreateStudentAsync(student);
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllStudentsAsync();
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

    public async Task<bool> DeleteStudentByIdAsync(Guid studentId)
    {
        var student = await _studentRepository.GetStudentByIdAsync(studentId);
        if (student is null)
        {
            throw new NotFoundException("Student is not found with the given ID.");
        }
        
        await _studentRepository.DeleteStudentAsync(studentId);
        return true;
    }

    public async Task<Student> GetStudentByIdAsync(Guid studentId)
    {
        var student = await _studentRepository.GetStudentByIdAsync(studentId);
        if (student is null)
        {
            throw new NotFoundException("Student is not found.");
        }

        return student;
    }

    public async Task<bool> UpdateStudentAsync(Student student)
    {
        if (student is null)
        {
            throw new ValidationException("Student updates cannot be null.");
        }

        var updatedStudent = await _studentRepository.GetStudentByIdAsync(student.Id);
        if (updatedStudent is null)
        {
            throw new NotFoundException("Student to update is not found.");
        }

        await _studentRepository.UpdateStudentAsync(student);
        return true;
    }

    public async Task<IEnumerable<Student>> GetStudentByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException("Search name cannot be empty.");
        }

        var students = await GetAllStudentsAsync();
        return students.Where(student => student.FirstName == name);
    }

    public async Task<int> GetStudentsCountAsync()
    {
        var students = await GetAllStudentsAsync();
        return students.Count();
    }

    public async Task AddStudentRangeAsync(params Student[] students)
    {
        if (students is null || students.Length == 0)
        {
            throw new ValidationException("Student list cannot be empty.");
        }

        foreach (var student in students)
        {
            await CreateStudentAsync(student);
        }
    }

    public async Task<IEnumerable<Student>> GetPaginatedStudentsAsync(int page, int pageSize)
    {
        if (page <= 0)
        {
            throw new ValidationException("Page number must be greater than zero.");
        }

        if (pageSize <= 0)
        {
            throw new ValidationException("Page size must be greater than zero.");
        }

        var students = await GetAllStudentsAsync();
        return students.Skip((page - 1) * pageSize).Take(pageSize);
    }
}