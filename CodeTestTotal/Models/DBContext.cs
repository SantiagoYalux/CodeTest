using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace CodeTestTotal.Models
{
    public class DBContext
    {
        /*In this Class, we are going to make all the necessary functions to handle our json files (Data files) */
        public List<Usuario> Usuarios { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Mascota> Mascotas { get; set; }
        public List<Vendedor> Vendedores { get; set; }
        public List<Pedido> Pedidos { get; set; }

        /*Jsons files Paths*/
        private string MascotasJsonPath = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Mascotas.json";
        private string UsuariosJsonPath = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Usuarios.json";
        private string ClientesJsonPath = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Clientes.json";
        private string VendedoresJsonPath = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Vendedores.json";
        private string PedidosJsonPath = "C:\\Users\\yalux\\source\\repos\\WEB CORE\\CodeTestTotal\\CodeTestTotal\\DataContent\\Pedidos.json";

        public DBContext()
        {
            FillUsuarios();
            FillClientes();
            FillMascotas();
            FillPedidos();
        }

        private async void FillUsuarios()
        {
            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(UsuariosJsonPath);

            /*deserialize the content and assign to the User list*/
            Usuarios = JsonSerializer.Deserialize<List<Usuario>>(context);
        }
        private async void FillClientes()
        {

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(ClientesJsonPath);

            /*deserialize the content and assign to the User list*/
            Clientes = JsonSerializer.Deserialize<List<Cliente>>(context);
        }

        private async void FillMascotas()
        {

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(MascotasJsonPath);

            /*deserialize the content and assign to the User list*/
            Mascotas = JsonSerializer.Deserialize<List<Mascota>>(context);
        }
        private async void FillPedidos()
        {

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(PedidosJsonPath);

            /*deserialize the content and assign to the User list*/
            if (context.Length > 0)
                Pedidos = JsonSerializer.Deserialize<List<Pedido>>(context);
            else
                Pedidos = new List<Pedido>();
        }
        private async void FillVendedores()
        {

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(VendedoresJsonPath);

            /*deserialize the content and assign to the User list*/
            Vendedores = JsonSerializer.Deserialize<List<Vendedor>>(context);
        }

        public async Task<bool> AddNewRegister(object newObject)
        {
            /**/
            bool resultado = false;
            var type = newObject.GetType();

            switch (type.Name)
            {
                case "Mascota":
                    /*Add the new register*/
                    Mascotas.Add((Mascota)newObject);
                    var JsonMascotas = JsonSerializer.Serialize(Mascotas);

                    System.IO.File.WriteAllText(MascotasJsonPath, JsonMascotas);
                    resultado = true;
                    break;
                case "Usuario":
                    Usuarios.Add((Usuario)newObject);
                    var JsonUsuarios = JsonSerializer.Serialize(Usuarios);

                    System.IO.File.WriteAllText(UsuariosJsonPath, JsonUsuarios);
                    resultado = true;
                    break;
                case "Vendedor":
                    Vendedores.Add((Vendedor)newObject);
                    var JsonVendedores = JsonSerializer.Serialize(Vendedores);

                    System.IO.File.WriteAllText(VendedoresJsonPath, JsonVendedores);
                    resultado = true;
                    break;
                case "Cliente":
                    Clientes.Add((Cliente)newObject);
                    var JsonClientes = JsonSerializer.Serialize(Clientes);

                    System.IO.File.WriteAllText(ClientesJsonPath, JsonClientes);
                    resultado = true;
                    break;
                case "Pedido":
                    Pedidos.Add((Pedido)newObject);
                    var JsonPedidos = JsonSerializer.Serialize(Pedidos);

                    System.IO.File.WriteAllText(PedidosJsonPath, JsonPedidos);
                    resultado = true;
                    break;

                default:
                    break;
            }

            return await Task.FromResult(resultado);
        }

    }
}
