using Models.Teachers;

namespace SchoolManagement2.Repositories.TeacherRepositories;

public interface ITeacherRepository
{
    void CreateTeacher(Teacher teacher);
    IEnumerable<Teacher> GetAllTeachers();
    Teacher GetTeacherById(Guid teacherId);
    void UpdateTeacher(Teacher teacher);
    void DeleteTeacher(Guid teacherId);
}