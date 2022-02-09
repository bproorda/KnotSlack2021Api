using System.ComponentModel.DataAnnotations;

namespace knotslack2022api.Models.Identity.DTO
{
    public class RegisterData
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "User";
    }
}
