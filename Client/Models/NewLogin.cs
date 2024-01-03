using System.ComponentModel.DataAnnotations;

namespace BevBuddyWebApp.Client.Models
{
    public class NewLogin
    {
        [Required]
        [MinLength(8, ErrorMessage = "Invalid username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(12, ErrorMessage = "Invalid password")]
        public string Password { get; set; } = string.Empty;
    }
}
