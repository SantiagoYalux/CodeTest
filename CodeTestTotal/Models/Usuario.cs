namespace CodeTestTotal.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? UsuarioUsername { get; set; }
        public string? UsuarioUsernameNormalizado { get; set; }
        public string? UsuarioPasswordHash { get; set; }
    }
}
