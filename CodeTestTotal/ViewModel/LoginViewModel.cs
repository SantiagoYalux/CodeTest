using System.ComponentModel.DataAnnotations;

namespace CodeTestTotal.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Username { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}
