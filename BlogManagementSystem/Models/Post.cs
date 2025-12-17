using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BlogManagementSystem.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public int ViewCount { get; set; }

        public Blog Blog { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

