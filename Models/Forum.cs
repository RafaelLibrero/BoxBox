using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxBox.Models
{
    [Table("Forums")]
    public class Forum
    {
        [Key]
        [Column("ForumID")]
        public int ForumId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
