using BlogManagementSystem.Models;
using BlogManagementSystem.Services;

namespace BlogManagementSystem.Views
{
    public class CommentView
    {
        private readonly CommentService _commentService;

        public CommentView(CommentService commentService)
        {
            _commentService = commentService;
        }

        public void AddComment()
        {
            Console.Write("Post Id: ");
            int postId = int.Parse(Console.ReadLine());

            Console.Write("Author: ");
            string author = Console.ReadLine();

            Console.Write("Comment: ");
            string text = Console.ReadLine();

            _commentService.AddComment(new Comment
            {
                PostId = postId,
                Author = author,
                CommentText = text
            });

            Console.WriteLine("Comment added!");
            Console.ReadKey();
        }
    }
}