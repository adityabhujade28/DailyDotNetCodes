using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogManagementSystem.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        [StringLength(500)]
        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;

        public Post Post { get; set; }
    }
}
