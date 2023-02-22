using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace CodeTestTotal.Models
{
    public class DBContext
    {
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly string _AbsolutePath = "";
        public DBContext(IWebHostEnvironment IWebHostEnvironment)
        {
            /*we use IWebHostEnvironment to know where is hosted our app*/
            _IWebHostEnvironment = IWebHostEnvironment;

            //Get absolute path to the directory that contains the application content files
            _AbsolutePath = _IWebHostEnvironment.ContentRootPath;

            MascotasJsonPath = Path.Combine(_AbsolutePath.ToString(), "DataContent", "Mascotas.json");
            UsuariosJsonPath = Path.Combine(_AbsolutePath.ToString(), "DataContent", "Usuarios.json");
            ClientesJsonPath = Path.Combine(_AbsolutePath.ToString(), "DataContent", "Clientes.json");
            VendedoresJsonPath = Path.Combine(_AbsolutePath.ToString(), "DataContent", "Vendedores.json");
            PedidosJsonPath = Path.Combine(_AbsolutePath.ToString(), "DataContent", "Pedidos.json");

            FillUsuarios();
            FillClientes();
            FillMascotas();
            FillPedidos();
            FillVendedores();
        }

        /*In this Class, we are going to make all the necessary functions to handle our json files (Data files) */
        public List<Usuario> Usuarios { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Mascota> Mascotas { get; set; }
        public List<Vendedor> Vendedores { get; set; }
        public List<Pedido> Pedidos { get; set; }

        /*Json files Path*/
        private string MascotasJsonPath = "";
        private string UsuariosJsonPath = "";
        private string ClientesJsonPath = "";
        private string VendedoresJsonPath = "";
        private string PedidosJsonPath = "";

        private async void FillUsuarios()
        {
            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(UsuariosJsonPath);

            /*deserialize the content and assign to the User list*/
            if (context.Length > 0)
                Usuarios = JsonSerializer.Deserialize<List<Usuario>>(context);
            else
                Usuarios = new List<Usuario>();
        }
        private async void FillClientes()
        {

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(ClientesJsonPath);

            /*deserialize the content and assign to the User list*/
            if (context.Length > 0)
                Clientes = JsonSerializer.Deserialize<List<Cliente>>(context);
            else
                Clientes = new List<Cliente>();
        }

        private async void FillMascotas()
        {

            /*Get context from Usuarios json file*/
            string context = await File.ReadAllTextAsync(MascotasJsonPath);

            /*deserialize the content and assign to the User list*/
            if (context.Length > 0)
                Mascotas = JsonSerializer.Deserialize<List<Mascota>>(context);
            else
                Mascotas = new List<Mascota>();
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
            if (context.Length > 0)
                Vendedores = JsonSerializer.Deserialize<List<Vendedor>>(context);
            else
                Vendedores = new List<Vendedor>();
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

        public async Task<bool> ModRegister(object newObject)
        {
            /**/
            bool resultado = false;
            var type = newObject.GetType();

            switch (type.Name)
            {
                case "Mascota":
                    /*Remove the register*/
                    Mascotas.RemoveAll(x => x.MascotaID == ((Mascota)newObject).MascotaID);

                    /*Add the Mod register*/
                    Mascotas.Add((Mascota)newObject);
                    var JsonMascotas = JsonSerializer.Serialize(Mascotas);

                    System.IO.File.WriteAllText(MascotasJsonPath, JsonMascotas);
                    resultado = true;
                    break;
                case "Usuario":
                    /*Remove the register*/
                    Usuarios.RemoveAll(x => x.UsuarioId == ((Usuario)newObject).UsuarioId);

                    /*Add the Mod register*/
                    Usuarios.Add((Usuario)newObject);
                    var JsonUsuarios = JsonSerializer.Serialize(Usuarios);

                    System.IO.File.WriteAllText(UsuariosJsonPath, JsonUsuarios);
                    resultado = true;
                    break;
                case "Vendedor":
                    /*Remove the register*/

                    /*Add the Mod register*/
                    Vendedores.Add((Vendedor)newObject);
                    var JsonVendedores = JsonSerializer.Serialize(Vendedores);

                    System.IO.File.WriteAllText(VendedoresJsonPath, JsonVendedores);
                    resultado = true;
                    break;
                case "Cliente":
                    /*Remove the register*/
                    //Clientes.RemoveAll(x => x.ClienteID == ((Cliente)newObject));

                    /*Add the Mod register*/
                    Clientes.Add((Cliente)newObject);
                    var JsonClientes = JsonSerializer.Serialize(Clientes);

                    System.IO.File.WriteAllText(ClientesJsonPath, JsonClientes);
                    resultado = true;
                    break;
                case "Pedido":
                    /*Remove the register*/
                    Pedidos.RemoveAll(x => x.PedidoID == ((Pedido)newObject).PedidoID);

                    /*Add the Mod register*/
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

        public async Task<int> GetLastId(object Object)
        {
            int resultado = 0;
            var type = Object.GetType();

            switch (type.Name)
            {
                case "Mascota":
                    resultado = Mascotas.Max(x=> x.MascotaID);
                    break;
                case "Usuario":
                    resultado = Usuarios.Max(x=> x.UsuarioId);
                    break;
                case "Vendedor":
                    resultado = Vendedores.Max(x=> x.VendedorID);
                    break;
                case "Cliente":
                    resultado = Clientes.Max(x=> x.ClienteID);
                    break;
                case "Pedido":
                    resultado = Pedidos.Max(x=> x.PedidoID);
                    break;

                default:
                    break;
            }

            return await Task.FromResult(resultado);
        }

    }
}
