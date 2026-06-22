using Models.Teachers;
using SchoolManagement2.Repositories.GenericRepositories;
using System.Linq;
using SchoolManagement2.Exeptions;

namespace Services.TeacherFile;

public class TeacherService : ITeacherService
{
    private readonly IRepository<Teacher> teacherRepository;

    public TeacherService()
    {
        teacherRepository = new Repository<Teacher>("teachers.json");
    }

    public void CreateTeacher(Teacher teacher)
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
        var isthere = GetAllTeachers().Contains(teacher);
        if (isthere)
        {
            teacher.Id = Guid.NewGuid();
        }

        teacherRepository.Create(teacher);
    }

    public IEnumerable<Teacher> GetAllTeachers()
    {
        return teacherRepository.GetAll();
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

    public bool DeleteTeacherById(Guid teacherId)
    {
        var teacher = teacherRepository.GetById(teacherId);
        if (teacher is null)
        {
            throw new NotFoundException("Teacher is not found with the given ID.");
        }
        
        teacherRepository.Delete(teacherId);

        return true;
    }

    public Teacher GetTeacherById(Guid teacherId)
    {
        var teacher = teacherRepository.GetById(teacherId);
        if (teacher is null)
        {
            throw new NotFoundException("Teacher is not found.");
        }

        return teacher;
    }

    public bool UpdateTeacher(Teacher teacher)
    {
        if (teacher is null)
        {
            throw new ValidationException("Teacher updates cannot be null.");
        }

        var updatedteacher = teacherRepository.GetById(teacher.Id);
        if (updatedteacher is null)
        {
            throw new NotFoundException("Teacher to update is not found.");
        }

        teacherRepository.Update(teacher);

        return true;
    }

    public IEnumerable<Teacher> GetTeacherByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException("Search name cannot be empty.");
        }

        var teachers = GetAllTeachers();
        return teachers.Where(teacher => teacher.FirstName == name);
    }

    public int GetTeachersCount()
    {
        var teachers = GetAllTeachers();
        return teachers.Count();
    }

    public void AddTeacherRange(params Teacher[] teachers)
    {
        if (teachers is null || teachers.Length == 0)
        {
            throw new ValidationException("Teacher list cannot be empty.");
        }

        foreach (var teacher in teachers)
        {
            CreateTeacher(teacher);
        }
    }

    public IEnumerable<Teacher> GetPaginatedTeachers(int page, int pageSize)
    {
        if (page <= 0)
        {
            throw new ValidationException("Page number must be greater than zero.");
        }

        if (pageSize <= 0)
        {
            throw new ValidationException("Page size must be greater than zero.");
        }

        var teachers = GetAllTeachers();
        return teachers.Skip((page - 1) * pageSize).Take(pageSize);
    }
}