using Models.Teachers;

namespace Services.TeacherFile;

public interface ITeacherService
{
    void CreateTeacher(Teacher teacher);
    IEnumerable<Teacher> GetAllTeachers();
    void PrintTeacher(Teacher teacher);
    Teacher GetTeacherById(Guid teacherId);
    bool UpdateTeacher(Teacher teacher);
    bool DeleteTeacherById(Guid teacherId);
     IEnumerable<Teacher> GetTeacherByName(string name);
    int GetTeachersCount();
    void AddTeacherRange(params Teacher[] teachers);
     IEnumerable<Teacher> GetPaginatedTeachers(int page, int pageSize);
}