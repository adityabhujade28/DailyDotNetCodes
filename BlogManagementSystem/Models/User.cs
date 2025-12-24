using System.Collections.Generic;

namespace BlogManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();
        public ICollection<UserFollower> Following { get; set; } = new List<UserFollower>();
    }
}