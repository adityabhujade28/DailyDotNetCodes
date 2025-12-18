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
        public string BlogTitle { get; set; }
        [Required]
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BlogTitle { get; set; }

        [Required]
        public string Author { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
