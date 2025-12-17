using BlogManagementSystem.Data;
using BlogManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementSystem.Services
{
    public class PostService
    {
        private readonly AppDbContext _db;

        public PostService(AppDbContext db)
        {
            _db = db;
        }

        public void AddPost(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public List<Post> GetPosts()
        {
            return _db.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _db.Posts
                      .Include(p => p.Comments)
                      .FirstOrDefault(p => p.PostId == id);
        }

        public void IncreaseView(int id)
        {
            var post = _db.Posts.Find(id);
            post.ViewCount++;
            _db.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = _db.Posts.Find(id);
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }
    }
}
