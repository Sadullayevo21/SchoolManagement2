using Models.Students;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.StudentFile;

public interface IStudentService
{
    Task CreateStudentAsync(Student student);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    void PrintStudent(Student student);
    Task<Student> GetStudentByIdAsync(Guid studentId);
    Task<bool> UpdateStudentAsync(Student student);
    Task<bool> DeleteStudentByIdAsync(Guid studentId);
    Task<IEnumerable<Student>> GetStudentByNameAsync(string name);
    Task<int> GetStudentsCountAsync();
    Task AddStudentRangeAsync(params Student[] students);
    Task<IEnumerable<Student>> GetPaginatedStudentsAsync(int page, int pageSize);
}