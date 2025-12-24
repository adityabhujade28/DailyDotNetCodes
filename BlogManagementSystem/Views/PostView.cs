using BlogManagementSystem.Models;
using BlogManagementSystem.Services;

namespace BlogManagementSystem.Views
{
    public class PostView
    {
        private readonly PostService _postService;
        private readonly BlogService _blogService;

        public PostView(PostService postService, BlogService blogService)
        {
            _postService = postService;
            _blogService = blogService;
        }

        public void AddPost(int userId)
        {
            // Show available blogs first
            var blogs = _blogService.GetBlogs();
            if (blogs.Count == 0)
            {
                Console.WriteLine("No blogs available. Please create a blog first.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Available Blogs:");
            foreach (var b in blogs)
            {
                Console.WriteLine($"{b.BlogId}: {b.BlogTitle}");
            }
            Console.Write("Blog Id: ");
            var blogIdInput = Console.ReadLine();
            if (!int.TryParse(blogIdInput, out int blogId))
            {
                Console.WriteLine("Invalid Blog Id.");
                Console.ReadKey();
                return;
            }

            // Validate BlogId exists using BlogService
            var blog = blogs.FirstOrDefault(b => b.BlogId == blogId);
            if (blog == null)
            {
                Console.WriteLine($"Blog with Id {blogId} does not exist.");
                Console.ReadKey();
                return;
            }

            Console.Write("Title: ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                Console.ReadKey();
                return;
            }

            Console.Write("Content: ");
            string? content = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Content cannot be empty.");
                Console.ReadKey();
                return;
            }

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
            // Show all posts with their IDs and titles first
            var posts = _postService.GetPosts();
            if (posts.Count == 0)
            {
                Console.WriteLine("No posts available.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Available Posts:");
            foreach (var p in posts)
            {
                Console.WriteLine($"{p.PostId}: {p.Title}");
            }
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

        // IncreaseView removed

        public void DeletePost(int userId)
        {
            // Only allow deleting posts from blogs owned by the user
            var userBlogs = _blogService.GetBlogs().Where(b => b.UserId == userId).Select(b => b.BlogId).ToList();
            var posts = _postService.GetPosts().Where(p => userBlogs.Contains(p.BlogId)).ToList();
            if (posts.Count == 0)
            {
                Console.WriteLine("You have no posts to delete.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Your Posts:");
            foreach (var p in posts)
            {
                Console.WriteLine($"{p.PostId}: {p.Title}");
            }
            Console.Write("Post Id: ");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int postId))
            {
                Console.WriteLine("Invalid Post Id.");
                Console.ReadKey();
                return;
            }
            if (!posts.Any(p => p.PostId == postId))
            {
                Console.WriteLine($"Post with ID {postId} does not exist or you do not have permission to delete it.");
                Console.ReadKey();
                return;
            }
            _postService.DeletePost(postId);
            Console.WriteLine("Post deleted successfully!");
            Console.ReadKey();
        }
    }
}