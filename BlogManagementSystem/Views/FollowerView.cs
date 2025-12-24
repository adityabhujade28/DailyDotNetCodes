using BlogManagementSystem.Services;
using BlogManagementSystem.Models;

namespace BlogManagementSystem.Views
{
    public class FollowerView
    {
        private readonly FollowerService _followerService;
        public FollowerView(FollowerService followerService)
        {
            _followerService = followerService;
        }

        public void FollowUser(int userId)
        {
            Console.Write("Enter UserId to follow: ");
            if (int.TryParse(Console.ReadLine(), out int followId))
            {
                _followerService.Follow(followId, userId);
                // Get username for display
                using (var db = new BlogManagementSystem.Data.AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == followId);
                    if (user != null)
                        Console.WriteLine($"You are now following: {user.UserName} (Id: {user.UserId})");
                    else
                        Console.WriteLine($"You are now following User {followId}");
                }
            }
            else
            {
                Console.WriteLine("Invalid UserId.");
            }
            Console.ReadKey();
        }

        public void UnfollowUser(int userId)
        {
            Console.Write("Enter UserId to unfollow: ");
            if (int.TryParse(Console.ReadLine(), out int followId))
            {
                _followerService.Unfollow(followId, userId);
                Console.WriteLine($"You have unfollowed User {followId}");
            }
            else
            {
                Console.WriteLine("Invalid UserId.");
            }
            Console.ReadKey();
        }

        public void ShowFollowers(int userId)
        {
            using (var db = new BlogManagementSystem.Data.AppDbContext())
            {
                var followers = db.UserFollowers
                    .Where(uf => uf.UserId == userId)
                    .Select(uf => uf.FollowerId)
                    .ToList();
                if (followers.Count == 0)
                {
                    Console.WriteLine("You have 0 followers.");
                }
                else
                {
                    Console.WriteLine($"You have {followers.Count} followers:");
                    foreach (var fid in followers)
                    {
                        var user = db.Users.FirstOrDefault(u => u.UserId == fid);
                        if (user != null)
                            Console.WriteLine($"- {user.UserName} (Id: {user.UserId})");
                    }
                }
            }
            Console.ReadKey();
        }

        public void ShowFollowing(int userId)
        {
            using (var db = new BlogManagementSystem.Data.AppDbContext())
            {
                var following = db.UserFollowers
                    .Where(uf => uf.FollowerId == userId)
                    .Select(uf => uf.UserId)
                    .ToList();
                if (following.Count == 0)
                {
                    Console.WriteLine("You are following 0 users.");
                }
                else
                {
                    Console.WriteLine($"You are following {following.Count} users:");
                    foreach (var uid in following)
                    {
                        var user = db.Users.FirstOrDefault(u => u.UserId == uid);
                        if (user != null)
                            Console.WriteLine($"- {user.UserName} (Id: {user.UserId})");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
