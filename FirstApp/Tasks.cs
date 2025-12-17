using System;

namespace CSharpPractice
{
    class Program
    {
        public static void Tasks()
        {
            bool exit = false;

            while (exit != true)
            {
                Console.WriteLine("1. Calculate Simple Interest");
                Console.WriteLine("2. Calculator");
                Console.WriteLine("3. Print Pattern");
                Console.WriteLine("4. Type Conversion ");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                int choice = ConvertToInt(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CalculateSimpleInterest();
                        break;

                    case 2:
                        Calculator();
                        break;

                    case 3:
                        PrintPattern();
                        break;

                    case 4:
                        TypeConversionDemo();
                        break;

                    case 5:
                        exit = true;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void CalculateSimpleInterest()
        {
            Console.Write("Enter Principal amount: ");
            double.TryParse(Console.ReadLine(), out double principal);

            Console.Write("Enter Rate of Interest: ");
            double.TryParse(Console.ReadLine(), out double rate);

            Console.Write("Enter Time (in years): ");
            double.TryParse(Console.ReadLine(), out double time);

            double simpleInterest = (principal * rate * time) / 100;

            Console.WriteLine($"Simple Interest = {simpleInterest}");
        }

        static void Calculator()
        {

            Console.Write("Enter first number: ");
            double.TryParse(Console.ReadLine(), out double num1);

            Console.Write("Enter second number: ");
            double.TryParse(Console.ReadLine(), out double num2);

            Console.Write("Enter operator [ +, -, *, / ]: ");
            char op = Console.ReadLine()[0];

            if (op == '+')
            {
                Console.WriteLine($"Result = {num1 + num2}");
            }
            else if (op == '-')
            {
                Console.WriteLine($"Result = {num1 - num2}");
            }
            else if (op == '*')
            {
                Console.WriteLine($"Result = {num1 * num2}");
            }
            else if (op == '/')
            {
                if (num2 != 0)
                    Console.WriteLine($"Result = {num1 / num2}");
                else
                    Console.WriteLine("Division by zero not allowed.");
            }
            else
            {
                Console.WriteLine("Invalid operator.");
            }
        }

        static void PrintPattern()
        {
            Console.Write("Enter number of rows: ");
            int rows = ConvertToInt(Console.ReadLine());

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= rows; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            for (int i = rows; i >= 1; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();

            }
        }


        static void TypeConversionDemo()
        {
            Console.Write("Enter a number as string: ");
            string input = Console.ReadLine();

            int intValue = ConvertToInt(input);
            double doubleValue = ConvertToDouble(input);

            Console.WriteLine($"integer value: {intValue}");
            Console.WriteLine($"double value: {doubleValue}");
        }

        static int ConvertToInt(string value)
        {
            int result;
            int.TryParse(value, out result);
            return result;
        }

        static double ConvertToDouble(string value)
        {
            double result;
            double.TryParse(value, out result);
            return result;
        }
    }
}
