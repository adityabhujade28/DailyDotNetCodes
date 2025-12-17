using BlogManagementSystem.Models;
using BlogManagementSystem.Services;

namespace BlogManagementSystem.Views
{
    public class PostView
    {
        private readonly PostService _postService;

        public PostView(PostService postService)
        {
            _postService = postService;
        }

        public void AddPost()
        {
            Console.Write("Blog Id: ");
            int blogId = int.Parse(Console.ReadLine());

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Content: ");
            string content = Console.ReadLine();

            _postService.AddPost(new Post
            {
                BlogId = blogId,
                Title = title,
                Content = content,
                PublishedDate = DateTime.Now
            });

            Console.WriteLine("Post added!");
            Console.ReadKey();
        }

        public void ViewPostDetails()
        {
            Console.Write("Post Id: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Post Id.");
                Console.ReadKey();
                return;
            }

            var post = _postService.GetPost(id);

            if (post == null)
            {
                Console.WriteLine("Post not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine(post.Title);
            Console.WriteLine(post.Content);

            if (post.Comments != null && post.Comments.Count > 0)
            {
                foreach (var c in post.Comments)
                    Console.WriteLine($"- {c.Author}: {c.CommentText}");
            }
            else
            {
                Console.WriteLine("No comments.");
            }

            Console.ReadKey();
        }

        public void IncreaseView()
        {
            Console.Write("Post Id: ");
            _postService.IncreaseView(int.Parse(Console.ReadLine()));
        }

        public void DeletePost()
        {
            Console.Write("Post Id: ");
            _postService.DeletePost(int.Parse(Console.ReadLine()));
        }
    }
}