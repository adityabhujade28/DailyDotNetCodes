namespace BlogManagementSystem.Views
{
    public class Menu
    {
        private readonly BlogView _blogView;
        private readonly PostView _postView;
        private readonly CommentView _commentView;

        public Menu(BlogView blogView, PostView postView, CommentView commentView)
        {
            _blogView = blogView;
            _postView = postView;
            _commentView = commentView;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Create Blog");
                Console.WriteLine("2. Add Post");
                Console.WriteLine("3. Add Comment");
                Console.WriteLine("4. View Blogs");
                Console.WriteLine("5. View Post Details");
                Console.WriteLine("6. Increase View Count");
                Console.WriteLine("7. Delete Post");
                Console.WriteLine("8. Delete Blog");
                Console.WriteLine("0. Exit");

                switch (Console.ReadLine())
                {
                    case "1": _blogView.CreateBlog(); break;
                    case "2": _postView.AddPost(); break;
                    case "3": _commentView.AddComment(); break;
                    case "4": _blogView.ViewBlogs(); break;
                    case "5": _postView.ViewPostDetails(); break;
                    case "6": _postView.IncreaseView(); break;
                    case "7": _postView.DeletePost(); break;
                    case "8": _blogView.DeleteBlog(); break;
                    case "0": return;
                }
            }
        }
    }

}