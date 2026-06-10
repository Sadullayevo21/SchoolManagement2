using Models.Teachers;

namespace Services.TeacherFile;

public interface ITeacherService
{
    void CreateTeacher(Teacher teacher);
    Dictionary<int, Teacher> GetAllTeachers();
    void PrintTeacher(Teacher teacher);
    Teacher GetTeacherById(Guid teacherId);
    void UpdateTeacher(Teacher teacher);
    void DeleteTeacherById(Guid teacherId);
    IEnumerable<KeyValuePair<int, Teacher>> GetTeacherByName(string name);
    int GetTeachersCount();
    void AddTeacherRange(params Teacher[] teachers);
    IEnumerable<KeyValuePair<int, Teacher>> GetPaginatedTeachers(int page, int pageSize);
}