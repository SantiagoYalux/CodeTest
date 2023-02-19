using System.Text.Json;

namespace CodeTestTotal.Models
{
    public class DBContext
    {
        /*In this Class, we are going to make all the necessary functions to handle our json files (Data files) */
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<Mascota> Mascotas { get; set; }
        public IEnumerable<Vendedor> Vendedores { get; set; }

        public DBContext()
        {
            FillUsuarios();
            FillClientes();
            FillMascotas();
        }

        private async void FillUsuarios()
        {
            string path = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Usuarios.json";

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(path);

            /*deserialize the content and assign to the User list*/
            Usuarios = JsonSerializer.Deserialize<IEnumerable<Usuario>>(context);
        }
        private async void FillClientes()
        {
            string path = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Clientes.json";

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(path);

            /*deserialize the content and assign to the User list*/
            Clientes = JsonSerializer.Deserialize<IEnumerable<Cliente>>(context);
        }

        private async void FillMascotas()
        {
            string path = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Mascotas.json";

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(path);

            /*deserialize the content and assign to the User list*/
            Mascotas = JsonSerializer.Deserialize<IEnumerable<Mascota>>(context);
        }
        private async void FillVendedores()
        {
            string path = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Vendedores.json";

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(path);

            /*deserialize the content and assign to the User list*/
            Vendedores = JsonSerializer.Deserialize<IEnumerable<Vendedor>>(context);
        }

    }
}
