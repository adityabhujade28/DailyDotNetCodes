using BlogManagementSystem.Data;
using BlogManagementSystem.Models;

namespace BlogManagementSystem.Services
{
    public class CommentService
    {
        private readonly AppDbContext _db;

        public CommentService(AppDbContext db)
        {
            _db = db;
        }

        public void AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }
    }
}
