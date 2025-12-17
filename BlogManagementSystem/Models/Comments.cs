using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementSystem.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Author { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public Post Post { get; set; }
    }
}
