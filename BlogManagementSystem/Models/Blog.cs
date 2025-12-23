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
        public string BlogTitle { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
