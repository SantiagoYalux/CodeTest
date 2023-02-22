using System.ComponentModel.DataAnnotations;

namespace CodeTestTotal.ViewModel
{
    public class AddSellerViewModel
    {
        [Required]
        public string VendedorNombre { get; set; }        
        [Required]
        public string VendedorApellido { get; set; }
        [Required]
        public DateTime VendedorFechaIncorporación { get; set; }
        [Required]
        public string VendedorUsername { get; set; }
        [Required]
        public string VendedorPassword { get; set; }
    }
}
