using BlogManagementSystem.Data;
using BlogManagementSystem.Services;
using BlogManagementSystem.Views;

class Program
{
    static void Main()
    {
        using var db = new AppDbContext();

   
        var blogService = new BlogService(db);
        var postService = new PostService(db);
        var commentService = new CommentService(db);

      
        var blogView = new BlogView(blogService);
        var postView = new PostView(postService);
        var commentView = new CommentView(commentService);

     
        var menu = new Menu(blogView, postView, commentView);
        menu.Run();
    }
}
