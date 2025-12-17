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
    }
}