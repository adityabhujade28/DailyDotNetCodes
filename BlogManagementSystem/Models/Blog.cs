using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementSystem.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Post> Posts { get; set; }
    }
}

