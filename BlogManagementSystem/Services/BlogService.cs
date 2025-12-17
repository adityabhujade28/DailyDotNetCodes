using BlogManagementSystem.Data;
using BlogManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementSystem.Services
{
    public class BlogService
    {
        private readonly AppDbContext _db;

        public BlogService(AppDbContext db)
        {
            _db = db;
        }

        public void CreateBlog(string title, string author)
        {
            _db.Blogs.Add(new Blog
            {
                BlogTitle = title,
                Author = author,
                CreatedDate = DateTime.Now
            });
            _db.SaveChanges();
        }

        public List<Blog> GetBlogs()
        {
            return _db.Blogs
                      .Include(b => b.Posts)
                      .ToList();
        }
    }
}
