using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxBox.Models
{
    [Table("Users")]

    public class User
    {
        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }
        [Column("LastAccess")]
        public DateTime LastAccess { get; set; }
        [Column("RolID")]
        public int RolId { get; set; }
        [Column("ProfilePicture")]
        public string ProfilePicture { get; set; }
        [Column("TotalPosts")]
        public int TotalPosts { get; set; }

    }
}
