using System;
using System.Collections.Generic;
namespace BlogManagementSystem.Models
{
Class Blog
{
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public int ViewCount { get; set; }

        public Blog Blog { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

