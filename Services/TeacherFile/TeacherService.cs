using Models.Teachers;
using SchoolManagement2.Repositories.GenericRepositories;

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
        var teacher = GetTeacherById(teacherId);
        if (teacher is null)
        {
            return false;
        }
        
        teacherRepository.Delete(teacherId);

        return true;
    }

    public Teacher GetTeacherById(Guid teacherId)
    {
        return teacherRepository.GetById(teacherId);
    }

    public bool UpdateTeacher(Teacher teacher)
    {
        var updatedteacher = GetTeacherById(teacher.Id);
        if (updatedteacher is null)
        {
            return false;
        }

        teacherRepository.Update(teacher);

        return true;
    }

    public IEnumerable<Teacher> GetTeacherByName(string name)
    {
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
        foreach (var teacher in teachers)
        {
            CreateTeacher(teacher);
        }
    }

    public IEnumerable<Teacher> GetPaginatedTeachers(int page, int pageSize)
    {
        var teachers = GetAllTeachers();
        return teachers.Skip((page - 1) * pageSize).Take(pageSize);
    }
}