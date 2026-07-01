using System.Linq;
using Models.Students;
using Models.Teachers;
using Services.StudentFile;
using Services.TeacherFile;
using SchoolManagement2.Exeptions;

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
    Console.WriteLine("17. Eng aqlli o'quvchini ko'rish");
    Console.WriteLine("18. Eng yosh o'quvchini ko'rish");
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
        case "17":
            Sclever();
            break;
        case "18":
            Syoungest();
            break;
        case "0":
            Exit();
            break;
        default:
            Error();
            break;
    }

    async Task List()
    {
        try
        {
            IEnumerable<Teacher> teachers = await teacherService.GetAllTeachersAsync();
            
            Console.WriteLine("\n*** Barcha Ustozlar Ro'yxati ***");
            foreach (var teacher in teachers)
            {
                teacherService.PrintTeacher(teacher);
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task New()
    {
        try
        {
            Console.WriteLine("\n*** Yangi ustoz qo'shish ***");
            
            var newTeacher = new Teacher();

            Console.Write("Ismini kiriting: ");
            newTeacher.FirstName = Console.ReadLine();

            Console.Write("Familyasini kiriting: ");
            newTeacher.LastName = Console.ReadLine();

            Console.Write("Manzilini kiriting: ");
            newTeacher.Address = Console.ReadLine();

            await teacherService.CreateTeacherAsync(newTeacher);
            
            Console.WriteLine($"\n{newTeacher.FirstName} muvaffaqiyatli qo'shildi!");
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Update()
    {
        try
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

            var isupdated = await teacherService.UpdateTeacherAsync(teacher);
            if (isupdated)
            {
                Console.WriteLine($"\n{teacher.FirstName} muvaffaqiyatli yangilandi!");
            }
            else
            {
                Console.WriteLine($"\n{teacher.FirstName} yangilanmadi!");
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Delete()
    {
        try
        {
            Console.Write("Teacher Id ni kiriting: ");

            Guid teacherId = Guid.Parse(Console.ReadLine());

            var isdeleted = await teacherService.DeleteTeacherByIdAsync(teacherId);
            if (isdeleted)
            {
                Console.WriteLine($"\n{teacherId} muvaffaqiyatli o'chirildi!");
            }
            else
            {
                Console.WriteLine($"\n{teacherId} o'chirilmadi!");
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Name()
    {
        try
        {
            Console.Write("Ustozni ismini kiriting: ");

            string teacherFirstname = Console.ReadLine();
            var teachers = await teacherService.GetTeacherByNameAsync(teacherFirstname);

            foreach(var teacher in teachers)
            {
                Console.WriteLine(teacher.Id);
                Console.WriteLine(teacher.FirstName);
                Console.WriteLine(teacher.LastName);
                Console.WriteLine(teacher.Address);
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Count()
    {
        try
        {
            int count = await teacherService.GetTeachersCountAsync();
            Console.WriteLine($"Ustozlar soni: {count}");
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task AddRange()
    {
        try
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
            await teacherService.AddTeacherRangeAsync(teachers);

            Console.WriteLine("Ustozlar ma'lumotlari muvaffaqiyatli qo'shildi");
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Paginated()
    {
        try
        {
            Console.Write("Qaysi sahifadagi ma'lumotni kormoqchisiz: ");
            int page = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nechta ma'lumot ko'rmoqchisiz; ");
            int pageSize = Convert.ToInt32(Console.ReadLine());
            
            var teachers = await teacherService.GetPaginatedTeachersAsync(page, pageSize);

            foreach(var teacher in teachers)
            {
                Console.WriteLine(teacher.Id);
                Console.WriteLine(teacher.FirstName);
                Console.WriteLine(teacher.LastName);
                Console.WriteLine(teacher.Address);
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Slist()
    {
        try
        {
            IEnumerable<Student> students = await studentService.GetAllStudentsAsync();
            
            Console.WriteLine("\n*** Barcha O'quvchilar Ro'yxati ***");
            foreach (var student in students)
            {
                studentService.PrintStudent(student);
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async void Snew()
    {
        try
        {
            Console.WriteLine("\n*** Yangi o'quvchi qo'shish ***");
            
            var newStudent = new Student();

            Console.Write("Ismini kiriting: ");
            newStudent.FirstName = Console.ReadLine();

            Console.Write("Familyasini kiriting: ");
            newStudent.LastName = Console.ReadLine();

            Console.Write("Manzilini kiriting: ");
            newStudent.Address = Console.ReadLine();

            await studentService.CreateStudentAsync(newStudent);
            
            Console.WriteLine($"\n{newStudent.FirstName} muvaffaqiyatli qo'shildi!");
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Supdate()
    {
        try
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

            var isupdated = await studentService.UpdateStudentAsync(student);
            if (isupdated)
            {
                Console.WriteLine($"\n{student.FirstName} muvaffaqiyatli yangilandi!");
            }
            else
            {
                Console.WriteLine($"\n{student.FirstName} yangilanmadi!");
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async void Sdelete()
    {
        try
        {
            Console.Write("Id ni kiriting: ");
            Guid studentId = Guid.Parse(Console.ReadLine());

            var isdeleted = await studentService.DeleteStudentByIdAsync(studentId);
            if (isdeleted)
            {
                Console.WriteLine($"\n{studentId} muvaffaqiyatli o'chirildi!");
            }
            else
            {
                Console.WriteLine($"\n{studentId} o'chirilmadi!");
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Sname()
    {
        try
        {
            Console.Write("O'quvchi ismini kiriting: ");

            string studentFirstname = Console.ReadLine();
            var students = await studentService.GetStudentByNameAsync(studentFirstname);

            foreach(var student in students)
            {
                Console.WriteLine(student.Id);
                Console.WriteLine(student.FirstName);
                Console.WriteLine(student.LastName);
                Console.WriteLine(student.Address);
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Scount()
    {
        try
        {
            int count = await studentService.GetStudentsCountAsync();
            Console.WriteLine($"Studentlar soni: {count}");
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task SAddRange()
    {
        try
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
            await studentService.AddStudentRangeAsync(students);

            Console.WriteLine("O'quvchilar ma'lumotlari muvaffaqiyatli qo'shildi");
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Spaginated()
    {
        try
        {
            Console.Write("Qaysi sahifadagi ma'lumotni kormoqchisiz: ");
            int page = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nechta ma'lumot ko'rmoqchisiz; ");
            int pageSize = Convert.ToInt32(Console.ReadLine());
            
            var students = await studentService.GetPaginatedStudentsAsync(page, pageSize);

            foreach(var student in students)
            {
                Console.WriteLine(student.Id);
                Console.WriteLine(student.FirstName);
                Console.WriteLine(student.LastName);
                Console.WriteLine(student.Address);
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }
        
        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Sclever()
    {
        try
        {
            IEnumerable<Student> allStudents = await studentService.GetAllStudentsAsync();
            var cleverStudent = allStudents.FindFirstOrDefaultCleverStudent();

            if (cleverStudent != null)
            {
                Console.WriteLine($"Eng aqlli o'quvchi: {cleverStudent.FirstName} {cleverStudent.LastName}");
            }
            else
            {
                Console.WriteLine("O'quvchilar ro'yxati bo'sh!");
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }

        Console.WriteLine("\nMenyoga qaytish uchun ixtiyoriy tugmani bosing...");
        Console.ReadKey();
    }

    async Task Syoungest()
    {
        try
        {
            IEnumerable<Student> allStudents = await studentService.GetAllStudentsAsync();
            var youngestStudent = allStudents.FindFirstOrDefaultYoungestStudent();
            
            if (youngestStudent != null)
            {
                Console.WriteLine($"Eng yosh o'quvchi: {youngestStudent.FirstName}");
            }
        }
        catch (ValidationException exception) { ShowError(exception.Message); }
        catch (NotFoundException exception) { ShowError(exception.Message); }
        catch (Exception exception) { Console.WriteLine(exception.Message); }

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

    void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nXatolik yuz berdi: {message}");
        Console.ResetColor();
    }

} while (choice != "0");