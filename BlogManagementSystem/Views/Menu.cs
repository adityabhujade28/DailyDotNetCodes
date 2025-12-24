namespace BlogManagementSystem.Views
{
    public class Menu
    {
        private readonly BlogView _blogView;
        private readonly PostView _postView;
        private readonly CommentView _commentView;
        private readonly UserView _userView;
        private readonly FollowerView _followerView;

        public Menu(BlogView blogView, PostView postView, CommentView commentView, UserView userView, FollowerView followerView)
        {
            _blogView = blogView;
            _postView = postView;
            _commentView = commentView;
            _userView = userView;
            _followerView = followerView;
        }

        public void Run()
        {
            while (true)
            {
                int userId = _userView.ShowLoginOrRegister();
                bool loggedIn = true;
                while (loggedIn)
                {
                    Console.Clear();
                    Console.WriteLine($"Logged in as UserId: {userId}");
                    Console.WriteLine("1. Create Blog");
                    Console.WriteLine("2. Add Post");
                    Console.WriteLine("3. Add Comment");
                    Console.WriteLine("4. View Blogs");
                    Console.WriteLine("5. View Post Details");
                    Console.WriteLine("6. Follow a User");
                    Console.WriteLine("7. Unfollow a User");
                    Console.WriteLine("8. Show My Followers");
                    Console.WriteLine("9. Show My Following");
                    Console.WriteLine("10. Delete Post");
                    Console.WriteLine("11. Delete Blog");
                    Console.WriteLine("12. Logout");

                    switch (Console.ReadLine())
                    {
                        case "1": _blogView.CreateBlog(userId); break;
                        case "2": _postView.AddPost(userId); break;
                        case "3": _commentView.AddComment(userId); break;
                        case "4": _blogView.ViewBlogs(); break;
                        case "5": _postView.ViewPostDetails(); break;
                        case "6": _followerView.FollowUser(userId); break;
                        case "7": _followerView.UnfollowUser(userId); break;
                        case "8": _followerView.ShowFollowers(userId); break;
                        case "9": _followerView.ShowFollowing(userId); break;
                        case "10": _postView.DeletePost(userId); break;
                        case "11": _blogView.DeleteBlog(userId); break;
                        case "12": loggedIn = false; break;
                    }
                }
            }
        }
    }

}