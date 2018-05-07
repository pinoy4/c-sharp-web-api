using System.ComponentModel.DataAnnotations;

namespace MWTest.Payloads
{
    public class JwtPostPayload
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
