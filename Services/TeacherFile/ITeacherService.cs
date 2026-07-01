using Models.Teachers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.TeacherFile;

public interface ITeacherService
{
    Task CreateTeacherAsync(Teacher teacher);
    Task<IEnumerable<Teacher>> GetAllTeachersAsync();
    void PrintTeacher(Teacher teacher);
    Task<Teacher> GetTeacherByIdAsync(Guid teacherId);
    Task<bool> UpdateTeacherAsync(Teacher teacher);
    Task<bool> DeleteTeacherByIdAsync(Guid teacherId);
    Task<IEnumerable<Teacher>> GetTeacherByNameAsync(string name);
    Task<int> GetTeachersCountAsync();
    Task AddTeacherRangeAsync(params Teacher[] teachers);
    Task<IEnumerable<Teacher>> GetPaginatedTeachersAsync(int page, int pageSize);
}