using CodeTestTotal.Models;

namespace CodeTestTotal.Interfaces
{
    public interface IClientService
    {
        List<Cliente> GetAllClients();
        Cliente GetClient(int UserID);
    }
}
