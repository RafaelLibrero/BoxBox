using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxBox.Models
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        [Column("TeamID")]
        public int TeamId { get; set; }
        [Column("TeamName")]
        public string TeamName { get; set; }
        [Column("Logo")]
        public string Logo { get; set; }
    }
}
