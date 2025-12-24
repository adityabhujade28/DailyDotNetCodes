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
    var userService = new UserService(db);
    var followerService = new FollowerService(db);

    var blogView = new BlogView(blogService);
    var postView = new PostView(postService, blogService);
    var commentView = new CommentView(commentService);
    var userView = new UserView(userService);
    var followerView = new FollowerView(followerService);

    var menu = new Menu(blogView, postView, commentView, userView, followerView);
    menu.Run();
    }
}
