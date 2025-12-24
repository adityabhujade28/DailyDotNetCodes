using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(150)]
        public string? BlogTitle { get; set; }

        // Remove Author string, use User reference
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<Post> Posts { get; set; } = new List<Post>();

        // Views count
        public int Views { get; set; } = 0;
    }
}
