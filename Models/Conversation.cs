using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxBox.Models
{
    [Table("Conversations")]
    public class Conversation
    {
        [Key]
        [Column("ConversationID")]
        public int ConversationId { get; set; }
        [Column("TopicID")]
        public int TopicId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
