using System;
using System.Collections.Generic;

namespace BlogManagementSystem.Models
{
public class Blog
{
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
