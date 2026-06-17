using Models.Teachers;

namespace SchoolManagement2.Repositories.TeacherRepositories;

public class TeacherRepository : JsonRepository<Teacher>
{
    public TeacherRepository() : base("teachers.json")
    {
    }
}