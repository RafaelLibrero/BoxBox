using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxBox.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        [Column("PostID")]
        public int PostId { get; set; }
        [Column("ThreadID")]
        public int ThreadId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("Text")]
        public string Text { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

    }
}
