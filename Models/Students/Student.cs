namespace Models.Students;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public int Grade { get; set; }
    public int Age { get; set; }
}
