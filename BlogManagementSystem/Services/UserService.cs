using BlogManagementSystem.Data;
using BlogManagementSystem.Models;
using System.Linq;

namespace BlogManagementSystem.Services
{
    public class UserService
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public User? Register(string userName, string email, string password)
        {
            if (_db.Users.Any(u => u.UserName == userName))
                return null;
            var user = new User { UserName = userName, Email = email, Password = password };
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User? Login(string userName, string password)
        {
            return _db.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }
    }
}