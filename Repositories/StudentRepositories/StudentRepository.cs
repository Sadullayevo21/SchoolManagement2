using Models.Students;

namespace SchoolManagement2.Repositories.StudentRepositories;

public class StudentRepository : JsonRepository<Student>
{
    public StudentRepository() : base("students.json")
    {
    }
}