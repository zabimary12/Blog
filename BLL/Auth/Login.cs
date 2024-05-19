using System.ComponentModel.DataAnnotations;

namespace BLL.Auth
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required] [StringLength(50)] public string Password { get; set; }
    }
}