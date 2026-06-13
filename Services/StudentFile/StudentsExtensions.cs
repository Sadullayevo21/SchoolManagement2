using Models.Students;
using System.Collections.Generic;
using System.Linq;

namespace Services.StudentFile;

public static class StudentsExtension
{
    public static Student FindFirstOrDefaultCleverStudent(this Dictionary<int, Student> studentDict)
    {
        if (studentDict == null || !studentDict.Any())
        {
            return null;
        }

        var maxGrade = studentDict.Values.Max(s => s.Grade);
        return studentDict.Values.FirstOrDefault(s => s.Grade == maxGrade);
    }

    public static Student FindFirstOrDefaultYoungestStudent(this Dictionary<int, Student> studentDict)
    {
        if (studentDict == null || !studentDict.Any())
        {
            return null;
        }

        var minAge = studentDict.Values.Min(s => s.Age);
        return studentDict.Values.FirstOrDefault(s => s.Age == minAge);
    }

    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
    {
        if (collection == null) return Enumerable.Empty<T>();

        return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}