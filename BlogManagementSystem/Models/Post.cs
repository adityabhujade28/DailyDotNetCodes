using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementSystem.Models
{
    public class Post
    {

        [Key]
        public int PostId { get; set; }

        [Required]
        public int BlogId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; } = DateTime.Now;

        public int ViewCount { get; set; } = 0;

        public Blog Blog { get; set; }
        
        public List<Comment> Comments { get; set; }
    }
}
