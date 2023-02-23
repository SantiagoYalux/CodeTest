using System.ComponentModel.DataAnnotations;

namespace CodeTestTotal.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
    }
}
