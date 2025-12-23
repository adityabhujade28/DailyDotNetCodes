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

        public void CreateBlog()
        {
            Console.Write("Blog Title: ");
            string title = Console.ReadLine();

            Console.Write("Author: ");
            string author = Console.ReadLine();

            _blogService.CreateBlog(title, author);

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

        public void DeleteBlog()
{
    Console.Clear();
    Console.WriteLine("=== Delete Blog ===");

    var blogs = _blogService.GetBlogs();

    if (blogs.Count == 0)
    {
        Console.WriteLine("No blogs available to delete.");
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

    _blogService.DeleteBlog(blogId);

    Console.WriteLine("Blog deleted successfully!");
    Console.ReadKey();
}

    }
}