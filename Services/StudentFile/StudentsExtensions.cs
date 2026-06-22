using Models.Students;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement2.Exeptions; 

namespace Services.StudentFile;

public static class StudentsExtension
{
    public static Student FindFirstOrDefaultCleverStudent(this IEnumerable<Student> studentDict)
    {
        if (studentDict == null || !studentDict.Any())
        {
            throw new NotFoundException("No students found in the list.");
        }

        var maxGrade = studentDict.Max(student => student.Grade);
        var cleverStudent = studentDict.FirstOrDefault(student => student.Grade == maxGrade);

        if (cleverStudent == null)
        {
            throw new NotFoundException("Clever student is not found.");
        }

        return cleverStudent;
    }

    public static Student FindFirstOrDefaultYoungestStudent(this IEnumerable<Student> studentDict)
    {
        if (studentDict == null || !studentDict.Any())
        {
            throw new NotFoundException("No students found in the list.");
        }

        var minAge = studentDict.Min(student => student.Age);
        var youngestStudent = studentDict.FirstOrDefault(student => student.Age == minAge);

        if (youngestStudent == null)
        {
            throw new NotFoundException("Youngest student is not found.");
        }

        return youngestStudent;
    }
}