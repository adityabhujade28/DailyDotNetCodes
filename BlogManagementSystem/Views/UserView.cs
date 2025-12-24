using BlogManagementSystem.Services;
using BlogManagementSystem.Models;

namespace BlogManagementSystem.Views
{
    public class UserView
    {
        private readonly UserService _userService;
        public UserView(UserService userService)
        {
            _userService = userService;
        }

        public int ShowLoginOrRegister()
        {
            while (true)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");
                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.Write("Username: ");
                    var username = Console.ReadLine();
                    Console.Write("Email: ");
                    var email = Console.ReadLine();
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    var user = _userService.Register(username!, email!, password!);
                    if (user == null)
                    {
                        Console.WriteLine("Username already exists. Try again.");
                        continue;
                    }
                    Console.WriteLine("Registration successful. Your UserId: " + user.UserId);
                    return user.UserId;
                }
                else if (choice == "2")
                {
                    Console.Write("Username: ");
                    var username = Console.ReadLine();
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    var user = _userService.Login(username!, password!);
                    if (user == null)
                    {
                        Console.WriteLine("Invalid credentials. Try again.");
                        continue;
                    }
                    Console.WriteLine("Login successful. Your UserId: " + user.UserId);
                    return user.UserId;
                }
                else if (choice == "3")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }
    }
}