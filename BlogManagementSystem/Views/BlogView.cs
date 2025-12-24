using BlogManagementSystem.Services;
namespace BlogManagementSystem.Views
{
    public class BlogView
    {
        private readonly BlogService _blogService;

        public BlogView(BlogService blogService)
        {
            _blogService = blogService;
        }

        public void CreateBlog(int userId)
        {
            Console.Write("Blog Title: ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Blog title cannot be empty.");
                return;
            }

            _blogService.CreateBlog(title, userId);

            Console.WriteLine("Blog created successfully!");
            Console.ReadKey();
        }

        public void ViewBlogs()
        {
            var blogs = _blogService.GetBlogs();

            foreach (var blog in blogs)
            {
                Console.WriteLine($"{blog.BlogId}. {blog.BlogTitle} (Posts: {blog.Posts.Count})");
            }

            Console.ReadKey();
        }

        public void DeleteBlog(int userId)
{
    Console.Clear();
    Console.WriteLine("=== Delete Blog ===");

    var blogs = _blogService.GetBlogs().Where(b => b.UserId == userId).ToList();

    if (blogs.Count == 0)
    {
        Console.WriteLine("You have no blogs to delete.");
        Console.ReadKey();
        return;
    }

    foreach (var blog in blogs)
    {
        Console.WriteLine($"{blog.BlogId}. {blog.BlogTitle} (Posts: {blog.Posts.Count})");
    }

    Console.Write("\nEnter Blog ID to delete: ");
    bool isValid = int.TryParse(Console.ReadLine(), out int blogId);

    if (!isValid)
    {
        Console.WriteLine("Invalid Blog ID.");
        Console.ReadKey();
        return;
    }

    var blogsList = blogs.Select(b => b.BlogId).ToList();
    if (!blogsList.Contains(blogId))
    {
        Console.WriteLine($"Blog with ID {blogId} does not exist.");
        Console.ReadKey();
        return;
    }

    _blogService.DeleteBlog(blogId);
    Console.WriteLine("Blog deleted successfully!");
    Console.ReadKey();
}

    }
}