using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using System.Linq;
namespace CodeTestTotal.Services
{
    public class ClientService : IClientService
    {
        private readonly DBContext _DbContext;
        public ClientService(DBContext DBContext)
        {
            _DbContext = DBContext;
        }

        public Cliente GetClient(int UserID)
        {
            return _DbContext.Clientes.FirstOrDefault(x => x.ClienteUsuarioID == UserID);
        }
        public Cliente GetClientByID(int ClientID)
        {
            return _DbContext.Clientes.FirstOrDefault(x => x.ClienteID == ClientID);
        }

        public List<Cliente> GetAllClients()
        {
            return _DbContext.Clientes;
        }

        public async Task<bool> AddClient(int userID, string ClientName)
        {
            bool result = false;
            Cliente newCLiente = new Cliente();
            newCLiente.ClienteID = await _DbContext.GetLastId(newCLiente) + 1;
            newCLiente.ClienteNombre = ClientName;
            newCLiente.ClienteUsuarioID = userID;

            result = await _DbContext.AddNewRegister(newCLiente);

            return result;
        }

    }
}
