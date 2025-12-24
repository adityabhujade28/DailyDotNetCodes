using BlogManagementSystem.Data;
using BlogManagementSystem.Models;
using System.Linq;

namespace BlogManagementSystem.Services
{
    public class FollowerService
    {
        private readonly AppDbContext _db;
        public FollowerService(AppDbContext db)
        {
            _db = db;
        }

        public void Follow(int userId, int followerId)
        {
            if (!_db.UserFollowers.Any(uf => uf.UserId == userId && uf.FollowerId == followerId))
            {
                _db.UserFollowers.Add(new UserFollower { UserId = userId, FollowerId = followerId });
                _db.SaveChanges();
            }
        }

        public void Unfollow(int userId, int followerId)
        {
            var uf = _db.UserFollowers.FirstOrDefault(uf => uf.UserId == userId && uf.FollowerId == followerId);
            if (uf != null)
            {
                _db.UserFollowers.Remove(uf);
                _db.SaveChanges();
            }
        }

        public int GetFollowersCount(int userId)
        {
            return _db.UserFollowers.Count(uf => uf.UserId == userId);
        }

        public int GetFollowingCount(int followerId)
        {
            return _db.UserFollowers.Count(uf => uf.FollowerId == followerId);
        }
    }
}
