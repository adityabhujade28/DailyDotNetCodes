using System;

class Program
{
    static void Main()
    {
        int number = 10;
        double price = 10.5;
        float g = 5.5f;
        string Name = "Rahul";
        bool isPresent = true;
        char Alphabet = 'A';
        Console.WriteLine(number + " " + price + " " + g + " " + Name + " " + Alphabet + " " + isPresent + " ");

        int a = 5;
        int b = a;
        b = 10;
        Console.WriteLine(b + " " + a);

        string numberText = "123";
        int parsedInt1 = Convert.ToInt32(numberText);
        int parsedInt2 = int.Parse(numberText);

        bool success = int.TryParse("abc", out int result);
        Console.WriteLine(success);
        int x = 10, y = 3;
        int ADD = x + y;
        int diff = x - y;
        int product = x * y;
        int division = x / y;
        int modulo = x % y;
        Console.WriteLine(ADD + " " + diff + " " + product + " " + division + " " + modulo);
        x++;
        y--;
        Console.WriteLine(x);
        Console.WriteLine(y);

        if (x > y)
        {
            Console.WriteLine("x > y");
        }
        else if (x == y)
        {
            Console.WriteLine("x == y");
        }
        else
        {
            Console.WriteLine("y > x");
        }

        Console.Write("Enter a day in range ( 1 - 7 ) :");
        string day = Console.ReadLine();
        switch (day)
        {
            case "1":
                Console.WriteLine("Monday");
                break;
            case "2":
                Console.WriteLine("Tuesday");
                break;
            case "3":
                Console.WriteLine("Wednesday");
                break;
            case "4":
                Console.WriteLine("Thrusday");
                break;
            case "5":
                Console.WriteLine("Friday");
                break;
            case "6":
                Console.WriteLine("Saturday");
                break;
            case "7":
                Console.WriteLine("Sunday");
                break;
            default:
                Console.WriteLine("Other day");
                break;
        }

        for (int i = 1; i <= 5; i++)
            Console.WriteLine($"Number: {i}");

        int counter = 1;
        while (counter < 6)
        {
            Console.WriteLine($"While Counter : {counter}");
            counter++;
        }

        int j = 0;
        do
        {
            Console.WriteLine($"Do-while: {j}");
            j++;
        } while (j < 3);

        string[] names = { "Aditya", "Aman", "Vijay" };
        foreach (var name in names)
        {
            if (name == "Aman")
                continue;

            Console.WriteLine(name);
        }

        CSharpPractice.Program.Tasks();
    }
}