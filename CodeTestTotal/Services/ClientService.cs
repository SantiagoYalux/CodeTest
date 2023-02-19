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
    }
}
