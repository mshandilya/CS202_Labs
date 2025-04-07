using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        Console.Write("Enter the part of the Lab you wish to execute\n");
        int part = Int32.Parse(Console.ReadLine());
        if (part == 0)
        {
            Console.Write("Oops! Did you mean part 1?");
        }
        else if (part == 1)
        {
            Part1 obj = new Part1();
            obj.execute();
        }
        else if (part == 2)
        {
            Part2 obj = new Part2();
            obj.execute();
        }
        else if (part == 3)
        {
            Part3 obj = new Part3();
            obj.execute();
        }
        else if (part == 4)
        {
            Student.Main();
            StudentIITGN.Main();
        }

    }
}

class Part1
{
    public void execute()
    {
        Console.Write("Hello Universe! Why just stay to the world?\n");
    }
}

class Part2
{
    public void execute()
    {
        Console.Write("Enter two numbers\n");
        int num1 = Int32.Parse(Console.ReadLine()), num2 = Int32.Parse(Console.ReadLine()), res;
        Console.Write("Enter an operator\n");
        string op = Console.ReadLine();
        try
        {
            if (op == "+")
            {
                res = add(num1, num2);
            }
            else if (op == "-")
            {
                res = subtract(num1, num2);
            }
            else if (op == "*")
            {
                res = multiply(num1, num2);
            }
            else if (op == "/")
            {
                res = divide(num1, num2);
            }
            else
            {
                Console.Write("Incorrect operator!\n");
                return;
            }
            Console.WriteLine(res);
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Please enter a non-zero divisor next time");
        }
    }

    int add(int x, int y)
    {
        return x + y;
    }

    int multiply(int x, int y)
    {
        return x * y;
    }

    int divide(int x, int y)
    {
        return x / y;
    }

    int subtract(int x, int y)
    {
        return x - y;
    }
}

class Part3
{
    public void execute()
    {
        Console.Write("Mention the subpart you wish to execute\n");
        int part = Int32.Parse(Console.ReadLine());
        if (part == 0)
        {
            Console.Write("Did you mean 1?");
        }
        else if (part == 1)
        {
            forloop();
        }
        else if (part == 2)
        {
            whileloop();
        }
        else if (part == 3)
        {
            Console.Write("Please enter a number\n");
            int num = Int32.Parse(Console.ReadLine());
            Console.WriteLine(factorial(num));
        }
        else
        {
            Console.Write("Incorrect option!\n");
        }
    }

    void forloop()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
    }

    void whileloop()
    {
        Console.Write("Please enter something\n");
        string entered = Console.ReadLine();
        while (entered != "exit")
        {
            Console.Write("Please enter something\n");
            entered = Console.ReadLine();
        }
    }

    int factorial(int num)
    {
        if (num < 0)
            return 0;
        if (num == 0)
            return 1;
        return num*factorial(num-1);
    }
}

class Student
{
    public string Name, ID;
    int Marks;

    public static void Main()
    {
        Student stud = new Student("Mrigankashekhar Shandilya", "22110157", 93),
            chad = new Student("Heer Kubadia", "22110096", 99),
            gigachad = new Student("Lavanya", "22110130", 95);

        Console.Write(format: "Name: {0}\nID: {1}\nGrade: {2}\n", stud.Name, stud.ID, stud.getGrade());
        Console.Write(format: "Name: {0}\nID: {1}\nGrade: {2}\n", chad.Name, chad.ID, chad.getGrade());
        Console.Write(format: "Name: {0}\nID: {1}\nGrade: {2}\n", gigachad.Name, gigachad.ID, gigachad.getGrade());
    }

    public Student(string name, string id, int marks)
    {
        this.Name = name;
        this.ID = id;
        this.Marks = marks;
    }

    public string getGrade()
    {
        if (Marks > 90)
            return "A";
        else if (Marks > 80)
            return "B";
        else if (Marks > 70)
            return "C";
        else
            return "D";
    }
}

class StudentIITGN : Student
{
    public string Hostel_Name_IITGN;
    
    new public static void Main()
    {
        StudentIITGN stud = new StudentIITGN("Mrigankashekhar Shandilya", "22110157", "Griwiksh", 93),
            chad = new StudentIITGN("Heer Kubadia", "22110096", "Chimair", 99),
            gigachad = new StudentIITGN("Lavanya", "22110130", "Chimair", 95);

        Console.Write(format: "Name: {0}\nID: {1}\nHostel: {2}\nGrade: {3}\n", stud.Name, stud.ID, stud.Hostel_Name_IITGN, stud.getGrade());
        Console.Write(format: "Name: {0}\nID: {1}\nHostel: {2}\nGrade: {3}\n", chad.Name, chad.ID, chad.Hostel_Name_IITGN, chad.getGrade());
        Console.Write(format: "Name: {0}\nID: {1}\nHostel: {2}\nGrade: {3}\n", gigachad.Name, gigachad.ID, gigachad.Hostel_Name_IITGN, gigachad.getGrade());
    }

    public StudentIITGN(string name, string id, string hostel, int marks) : base(name, id, marks)
    {
        this.Hostel_Name_IITGN = hostel;
    }
}