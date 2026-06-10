using System.Linq;
using Models.Students;
using Models.Teachers;
using Services.StudentFile;
using Services.TeacherFile;

ITeacherService teacherService = new TeacherService();
IStudentService studentService = new StudentService(); 

string choice;

do
{
    Console.Clear();
    
    Console.WriteLine("--- School Management Menu ---");
    Console.WriteLine("1. Ustozlar ro'yxatini ko'rish");
    Console.WriteLine("2. Yangi ustoz qo'shish");
    Console.WriteLine("3. Ustozni yangilash");
    Console.WriteLine("4. Ustozni o'chirish");
    Console.WriteLine("5. Ustozni ismi orqali izlash");
    Console.WriteLine("6. Ustozlarni sonini bilish");
    Console.WriteLine("7. Ko'p ustozlar qo'shish");
    Console.WriteLine("8. Ustozlarni sahifalarda ko'rish");
    Console.WriteLine("9. O'quvchilar ro'yxatini ko'rish");
    Console.WriteLine("10. Yangi o'quvchi qo'shish");
    Console.WriteLine("11. O'quvchini yangilash");
    Console.WriteLine("12. O'quvchini o'chirish");
    Console.WriteLine("13. O'quvchini ismi orqali izlash");
    Console.WriteLine("14. O'quvchilarni sonini bilish");
    Console.WriteLine("15. Ko'p o'quvchilar qo'shish");
    Console.WriteLine("16. O'quvchilarni sahifalarda ko'rish");
    Console.WriteLine("0. Chiqish");
    Console.Write("Tanlovingizni kiriting: ");
    
    choice = Console.ReadLine();

    switch (choice)
    {
        case "1": 
            List();
            break;
        case "2":
            New();
            break;
        case "3":
            Update();
            break;
        case "4":
            Delete();
            break;
        case "5":
            Name();
            break;
        case "6":
            Count();
            break;
        case "7":
            AddRange();
            break;
        case "8":
            Paginated();
            break;
        case "9":
            Slist();
            break;
        case "10":
            Snew();
            break;
        case "11":
            Supdate();
            break;
        case "12":
            Sdelete();
            break;
        case "13":
            Sname();
            break;
        case "14":
            Scount();
            break;
        case "15":
            SAddRange();
            break;
        case "16":
            Spaginated();
            break;
        case "0":
            Exit();
            break;
        default:
            Error();
            break;
    }

    void List()
    {
        Dictionary<int, Teacher> teachers = teacherService.GetAllTeachers();
        
        Console.WriteLine("\n*** Barcha Ustozlar Ro'yxati ***");
        foreach (var teacher in teachers)
        {
            teacherService.PrintTeacher(teacher.Value);
        }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void New()
    {
        Console.WriteLine("\n*** Yangi ustoz qo'shish ***");
        
        var newTeacher = new Teacher();

        Console.Write("Ismini kiriting: ");
        newTeacher.FirstName = Console.ReadLine();

        Console.Write("Familyasini kiriting: ");
        newTeacher.LastName = Console.ReadLine();

        Console.Write("Manzilini kiriting: ");
        newTeacher.Address = Console.ReadLine();

        teacherService.CreateTeacher(newTeacher);
        
        Console.WriteLine($"\n{newTeacher.FirstName} muvaffaqiyatli qo'shildi!");
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Update()
    {
        Teacher teacher = new Teacher();

        Console.Write("Id ni kiriting: ");
        teacher.Id = Guid.Parse(Console.ReadLine());

        Console.Write("Ismini kiriting: ");
        teacher.FirstName = Console.ReadLine();

        Console.Write("Familiyani kiriting: ");
        teacher.LastName = Console.ReadLine();

        Console.Write("Adressni kiriting: ");
        teacher.Address = Console.ReadLine();

        teacherService.UpdateTeacher(teacher);

        
        Console.WriteLine($"\n{teacher.FirstName} muvaffaqiyatli yangilandi!");
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Delete()
    {
        Console.Write("Teacher Id ni kiriting: ");

        Guid teacherId = Guid.Parse(Console.ReadLine());

        teacherService.DeleteTeacherById(teacherId); 

        Console.WriteLine($"\n{teacherId} muvaffaqiyatli o'chirildi!");

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Name()
    {
        Console.Write("Ustozni ismini kiriting: ");

        string teacherFirstname = Console.ReadLine();
        var teachers = teacherService.GetTeacherByName(teacherFirstname);

        foreach(var teacher in teachers)
        {
            Console.WriteLine(teacher.Value.Id);
            Console.WriteLine(teacher.Value.FirstName);
            Console.WriteLine(teacher.Value.LastName);
            Console.WriteLine(teacher.Value.Address);
        }

        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Count()
    {
        int count = teacherService.GetTeachersCount();

        Console.WriteLine($"Ustozlar soni: {count}");

        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void AddRange()
    {
        Teacher[] teachers = 
        [
            new Teacher
            {
                Id = Guid.NewGuid(),
                FirstName = "Asilbek",
                LastName = "Uchqunov",
                Address = "Toshkent"
            },
            new Teacher
            {
                Id = Guid.Parse("3bc86ae4-c475-4355-9b97-2cd8ed52eece"),
                FirstName = "Muhammadrizo",
                LastName = "Sodiqov",
                Address = "Toshkent"
            },
            new Teacher
            {
                Id = Guid.Parse("95413238-8c7c-413b-83aa-6e513c88b4df"),
                FirstName = "Mansur",
                LastName = "Akbarovhh",
                Address = "Toshkent"
            }
        ];
        teacherService.AddTeacherRange(teachers);

        Console.WriteLine("Ustozlar ma'lumotlari muvaffaqiyatli qo'shildi");

        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Paginated()
    {
        Console.Write("Qaysi sahifadagi ma'lumotni kormoqchisiz: ");
        
        int page = Convert.ToInt32(Console.ReadLine());

        Console.Write("Nechta ma'lumot ko'rmoqchisiz; ");

        int pageSize = Convert.ToInt32(Console.ReadLine());
        
        var teachers = teacherService.GetPaginatedTeachers(page, pageSize);

        foreach(var teacher in teachers)
        {
            Console.WriteLine(teacher.Value.Id);
            Console.WriteLine(teacher.Value.FirstName);
            Console.WriteLine(teacher.Value.LastName);
            Console.WriteLine(teacher.Value.Address);
        }

        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Slist()
    {
        Dictionary<int, Student> students = studentService.GetAllStudents();
        
        Console.WriteLine("\n*** Barcha O'quvchilar Ro'yxati ***");
        foreach (var student in students)
        {
            studentService.PrintStudent(student.Value);
        }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Snew()
    {
        Console.WriteLine("\n*** Yangi o'quvchi qo'shish ***");
        
        var newStudent = new Student();

        Console.Write("Ismini kiriting: ");
        newStudent.FirstName = Console.ReadLine();

        Console.Write("Familyasini kiriting: ");
        newStudent.LastName = Console.ReadLine();

        Console.Write("Manzilini kiriting: ");
        newStudent.Address = Console.ReadLine();

        studentService.CreateStudent(newStudent);
        
        Console.WriteLine($"\n{newStudent.FirstName} muvaffaqiyatli qo'shildi!");
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Supdate()
    {
        Student student = new Student();

        Console.Write("Id ni kiriting: ");
        student.Id = Guid.Parse(Console.ReadLine());

        Console.Write("Ismni kiriting: ");
        student.FirstName = Console.ReadLine();

        Console.Write("Familiyani kiriting: ");
        student.LastName = Console.ReadLine();

        Console.Write("Adressni kiriting: ");
        student.Address = Console.ReadLine();

        studentService.UpdateStudent(student);

        Console.WriteLine($"\n{student.FirstName} muvaffaqiyatli yangilandi!");

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Sdelete()
    {
        Console.Write("Id ni kiriting: ");
        
        Guid studentId = Guid.Parse(Console.ReadLine());

        studentService.DeleteStudentById(studentId);

        Console.WriteLine($"\n{studentId} muvaffaqiyatli o'chirildi!");

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Sname()
    {
        Console.Write("O'quvchi ismini kiriting: ");

        string studentFirstname = Console.ReadLine();
        var students = studentService.GetStudentByName(studentFirstname);

        foreach(var student in students)
        {
            Console.WriteLine(student.Value.Id);
            Console.WriteLine(student.Value.FirstName);
            Console.WriteLine(student.Value.LastName);
            Console.WriteLine(student.Value.Address);
        }

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Scount()
    {
        int count = studentService.GetStudentsCount();

        Console.WriteLine($"Studentlar soni: {count}");

        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void SAddRange()
    {
        Student[] students = 
        [
            new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Javohir",
                LastName = "Sodiqov",
                Address = "Toshkent"
            },
            new Student
            {
                Id = Guid.Parse("3dc2988f-4345-4266-88d0-36f9bc121ff0"),
                FirstName = "Abubakir",
                LastName = "Rahmonov",
                Address = "Toshkent"
            },
            new Student
            {
                Id = Guid.Parse("a4437ee1-2703-435d-a0ad-ba69c537b6b2"),
                FirstName = "Adham",
                LastName = "Munavvarov",
                Address = "Toshkent"
            }
        ];
        studentService.AddStudentRange(students);

        Console.WriteLine("O'quvchilar ma'lumotlari muvaffaqiyatli qo'shildi");

        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Spaginated()
    {
        Console.Write("Qaysi sahifadagi ma'lumotni kormoqchisiz: ");
        
        int page = Convert.ToInt32(Console.ReadLine());

        Console.Write("Nechta ma'lumot ko'rmoqchisiz; ");

        int pageSize = Convert.ToInt32(Console.ReadLine());
        
        var students = studentService.GetPaginatedStudents(page, pageSize);

        foreach(var student in students)
        {
            Console.WriteLine(student.Value.Id);
            Console.WriteLine(student.Value.FirstName);
            Console.WriteLine(student.Value.LastName);
            Console.WriteLine(student.Value.Address);
        }

        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    void Exit()
    {
        Console.WriteLine("Dastur tugatildi. Sog' bo'ling!");
    }

    void Error()
    {
        Console.WriteLine("Noto'g'ri buyruq kiritdingiz. Davom etish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

} while (choice != "0");