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

        public void CreateBlog(string title, int userId)
        {
            _db.Blogs.Add(new Blog
            {
                BlogTitle = title,
                UserId = userId,
                CreatedDate = DateTime.Now
            });
            _db.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var blog = _db.Blogs.Find(blogId);

                if (blog == null)
                    return;
                _db.Blogs.Remove(blog);
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
