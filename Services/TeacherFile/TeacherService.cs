using Models.Teachers;

namespace Services.TeacherFile;

public class TeacherService : ITeacherService
{
    private Dictionary<int, Teacher> teachers;   
    private int count = 0;

    public TeacherService()
    {
        teachers = new Dictionary<int, Teacher>()
        {
            {
                1, new Teacher
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Azizbek",
                    LastName = "Salimov",
                    Address = "Toshkent"
                }
            },
            {
                2, new Teacher
                {
                    Id = Guid.Parse("3bc86ae4-c475-4355-9b97-2cd8ed52eece"),
                    FirstName = "Nodir",
                    LastName = "Odilov",
                    Address = "Toshkent"
                }
            },
            {
                3, new Teacher
                {
                    Id = Guid.Parse("95413238-8c7c-413b-83aa-6e513c88b4df"),
                    FirstName = "Abror",
                    LastName = "Orifov",
                    Address = "Toshkent"
                }
            }
        };
    }

    private int IndexOfDictionary = 4;

    public void CreateTeacher(Teacher teacher)
    {
        teachers.Add(IndexOfDictionary, teacher);
        IndexOfDictionary++;
    }

    public Dictionary<int, Teacher> GetAllTeachers()
    {
        return this.teachers;
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
    
    public void DeleteTeacherById(Guid teacherId)
    {
        foreach(var teacher in teachers)
        {
            if(teacher.Value.Id == teacherId)
            {
                teachers.Remove(teacher.Key);
                return;
            }
        }
    }

    public Teacher GetTeacherById(Guid teacherId)
    {
        foreach(var teacher in teachers)
        {
            if (teacher.Value.Id == teacherId)
            {
                return teacher.Value;
            }
        }

        return null;
    }

    public void UpdateTeacher(Teacher teacher)
    {
       foreach(var newTeacher in teachers)
        {
            if(newTeacher.Value.Id == teacher.Id)
            {
                teachers.Remove(newTeacher.Key);
                teachers.Add(newTeacher.Key, teacher);
                return;
            }
        }
    }

    public IEnumerable<KeyValuePair<int, Teacher>> GetTeacherByName(string name)
    {
        return teachers.Where(teacher =>  teacher.Value.FirstName == name);
    }

    public int GetTeachersCount()
    {
        return teachers.Count();
    }

    public void AddTeacherRange(params Teacher[] teachers)
    {
        foreach(var teacher in teachers)
        {
            CreateTeacher(teacher);
        }
    }

    public IEnumerable<KeyValuePair<int, Teacher>> GetPaginatedTeachers(int page, int pageSize)
    {
        return teachers.Skip((page -1) * pageSize).Take(pageSize);
    }
}