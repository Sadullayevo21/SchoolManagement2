using Models.Teachers;
using SchoolManagement2.Repositories.GenericRepositories;
using System.Linq;
using SchoolManagement2.Exeptions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Services.TeacherFile;

public class TeacherService : ITeacherService
{
    private readonly IRepository<Teacher> teacherRepository;

    public TeacherService()
    {
        teacherRepository = new Repository<Teacher>("teachers.json");
    }

    public async Task CreateTeacherAsync(Teacher teacher)
    {
        if (teacher is null)
        {
            throw new ValidationException("Teacher data cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(teacher.FirstName))
        {
            throw new ValidationException("Teacher's first name is required.");
        }

        if (string.IsNullOrWhiteSpace(teacher.LastName))
        {
            throw new ValidationException("Teacher's last name is required.");
        }

        teacher.Id = Guid.NewGuid();
        var teachers = await GetAllTeachersAsync();
        var isthere = teachers.Contains(teacher);
        if (isthere)
        {
            teacher.Id = Guid.NewGuid();
        }

        await teacherRepository.CreateAsync(teacher);
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
    {
        return await teacherRepository.GetAllAsync();
    }

    public void PrintTeacher(Teacher teacher)
    {
        if (teacher is null)
        {
            throw new ValidationException("Cannot print null teacher details.");
        }

        Console.WriteLine("==========================");
        Console.WriteLine(
        $"""
        Teacher Info:
            First name: {teacher.FirstName}
            Last name: {teacher.LastName}
            Adress: {teacher.Address}
        """
        );
    }

    public async Task<bool> DeleteTeacherByIdAsync(Guid teacherId)
    {
        var teacher = await teacherRepository.GetByIdAsync(teacherId);
        if (teacher is null)
        {
            throw new NotFoundException("Teacher is not found with the given ID.");
        }
        
        await teacherRepository.DeleteAsync(teacherId);

        return true;
    }

    public async Task<Teacher> GetTeacherByIdAsync(Guid teacherId)
    {
        var teacher = await teacherRepository.GetByIdAsync(teacherId);
        if (teacher is null)
        {
            throw new NotFoundException("Teacher is not found.");
        }

        return teacher;
    }

    public async Task<bool> UpdateTeacherAsync(Teacher teacher)
    {
        if (teacher is null)
        {
            throw new ValidationException("Teacher updates cannot be null.");
        }

        var updatedteacher = await teacherRepository.GetByIdAsync(teacher.Id);
        if (updatedteacher is null)
        {
            throw new NotFoundException("Teacher to update is not found.");
        }

        await teacherRepository.UpdateAsync(teacher);

        return true;
    }

    public async Task<IEnumerable<Teacher>> GetTeacherByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException("Search name cannot be empty.");
        }

        var teachers = await GetAllTeachersAsync();
        return teachers.Where(teacher => teacher.FirstName == name);
    }

    public async Task<int> GetTeachersCountAsync()
    {
        var teachers = await GetAllTeachersAsync();
        return teachers.Count();
    }

    public async Task AddTeacherRangeAsync(params Teacher[] teachers)
    {
        if (teachers is null || teachers.Length == 0)
        {
            throw new ValidationException("Teacher list cannot be empty.");
        }

        foreach (var teacher in teachers)
        {
            await CreateTeacherAsync(teacher);
        }
    }

    public async Task<IEnumerable<Teacher>> GetPaginatedTeachersAsync(int page, int pageSize)
    {
        if (page <= 0)
        {
            throw new ValidationException("Page number must be greater than zero.");
        }

        if (pageSize <= 0)
        {
            throw new ValidationException("Page size must be greater than zero.");
        }

        var teachers = await GetAllTeachersAsync();
        return teachers.Skip((page - 1) * pageSize).Take(pageSize);
    }
}