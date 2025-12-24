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

        public void AddComment(int userId)
        {
            Console.Write("Post Id: ");
            var postIdInput = Console.ReadLine();
            if (!int.TryParse(postIdInput, out int postId))
            {
                Console.WriteLine("Invalid Post Id.");
                return;
            }

            Console.Write("Comment: ");
            string? text = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Comment cannot be empty.");
                return;
            }

            _commentService.AddComment(new Comment
            {
                PostId = postId,
                Author = userId.ToString(), // Optionally, fetch username by userId
                CommentText = text
            });

            Console.WriteLine("Comment added!");
            Console.ReadKey();
        }
    }
}