using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxBox.Models
{
    [Table("ThreadId")]
    public class Thread
    {
        [Key]
        [Column("ThreadID")]
        public int ThreadId { get; set; }
        [Column("ForumID")]
        public int ForumId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
