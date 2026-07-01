using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Students;

namespace SchoolManagement2.Repositories.StudentRepositories;

public interface IStudentRepository
{
    Task CreateStudentAsync(Student student);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(Guid studentId);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(Guid studentId);
}