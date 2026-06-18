using Models.Students;
using System.Collections.Generic;
using System.Linq;

namespace Services.StudentFile;

public static class StudentsExtension
{
    public static Student FindFirstOrDefaultCleverStudent(this IEnumerable<Student> studentDict)
    {
        if (studentDict == null || !studentDict.Any())
        {
            return null;
        }

        var maxGrade = studentDict.Max(student => student.Grade);
        return studentDict.FirstOrDefault(student => student.Grade == maxGrade);
    }

    public static Student FindFirstOrDefaultYoungestStudent(this IEnumerable<Student> studentDict)
    {
        if (studentDict == null || !studentDict.Any())
        {
            return null;
        }

        var minAge = studentDict.Min(student => student.Age);
        return studentDict.FirstOrDefault(student => student.Age == minAge);
    }
}