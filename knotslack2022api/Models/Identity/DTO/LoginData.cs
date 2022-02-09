using System.ComponentModel.DataAnnotations;

namespace knotslack2022api.Models.Identity.DTO
{
    public class LoginData
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
