using Models.Teachers;
using SchoolManagement2.Repositories.TeacherRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.TeacherFile;

public class TeacherService : ITeacherService
{
    private readonly TeacherRepository _teacherRepository;

    public TeacherService()
    {
        _teacherRepository = new TeacherRepository();

        if (!_teacherRepository.GetAll().Any())
        {
            _teacherRepository.Create(new Teacher
            {
                Id = Guid.NewGuid(),
                FirstName = "Azizbek",
                LastName = "Salimov",
                Address = "Toshkent"
            });
            _teacherRepository.Create(new Teacher
            {
                Id = Guid.Parse("3bc86ae4-c475-4355-9b97-2cd8ed52eece"),
                FirstName = "Nodir",
                LastName = "Odilov",
                Address = "Toshkent"
            });
            _teacherRepository.Create(new Teacher
            {
                Id = Guid.Parse("95413238-8c7c-413b-83aa-6e513c88b4df"),
                FirstName = "Abror",
                LastName = "Orifov",
                Address = "Toshkent"
            });
        }
    }

    public void CreateTeacher(Teacher teacher)
    {
        if (teacher.Id == Guid.Empty)
        {
            teacher.Id = Guid.NewGuid();
        }
        _teacherRepository.Create(teacher);
    }

    public Dictionary<int, Teacher> GetAllTeachers()
    {
        int index = 1;
        return _teacherRepository.GetAll().ToDictionary(teacher => index++, teacher => teacher);
    }

    public Teacher GetTeacherById(Guid teacherId)
    {
        return _teacherRepository.GetAll().FirstOrDefault(teacher => teacher.Id == teacherId);
    }

    public void UpdateTeacher(Teacher teacher)
    {
        _teacherRepository.Update(teacher => teacher.Id == teacher.Id, teacher);
    }

    public void DeleteTeacherById(Guid teacherId)
    {
        _teacherRepository.Delete(teacher => teacher.Id == teacherId);
    }

    public void PrintTeacher(Teacher teacher)
    {
        Console.WriteLine("==========================");
        Console.WriteLine(
        $"""
        Teacher Info:
            Id: {teacher.Id}
            First name: {teacher.FirstName}
            Last name: {teacher.LastName}
            Adress: {teacher.Address}
        """
        );
    }

    public IEnumerable<KeyValuePair<int, Teacher>> GetTeacherByName(string name)
    {
        return GetAllTeachers().Where(teacher => teacher.Value.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public int GetTeachersCount()
    {
        return _teacherRepository.GetAll().Count();
    }

    public void AddTeacherRange(params Teacher[] teachers)
    {
        foreach (var teacher in teachers)
        {
            CreateTeacher(teacher);
        }
    }

    public IEnumerable<KeyValuePair<int, Teacher>> GetPaginatedTeachers(int page, int pageSize)
    {
        return GetAllTeachers().Skip((page - 1) * pageSize).Take(pageSize);
    }
}