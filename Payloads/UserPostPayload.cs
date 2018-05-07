using MWTest.Model;
using System.ComponentModel.DataAnnotations;

namespace MWTest.Payloads
{
    public class UserPostPayload
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}