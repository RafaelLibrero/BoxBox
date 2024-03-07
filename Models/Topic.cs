using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxBox.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        [Column("TopicID")]
        public int TopicId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
