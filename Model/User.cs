using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MWTest.Model
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Column("password")]
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        [Column("username")]
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Column("role")]
        [Required]
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Guest = 0,
        User = 1,
        Admin = 2,
        Developer = 3
    }
}
