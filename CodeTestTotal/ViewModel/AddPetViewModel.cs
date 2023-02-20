using System.ComponentModel.DataAnnotations;

namespace CodeTestTotal.ViewModel
{
    public class AddPetViewModel
    {
        [Required]
        public string MascotaNombre { get; set; }
        [Required]
        public string MascotaTipo { get; set; }
        public string? MascotaImg { get; set; }
        [Required]
        public string MascotaDescrip { get; set; }
        [Required]
        public int MascotaEdad { get; set; }
        [Required]
        public int MascotaPeso { get; set; }
        [Required]
        public string MascotaCastrado { get; set; }
    }
}
