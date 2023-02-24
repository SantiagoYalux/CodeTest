using CodeTestTotal.Models;

namespace CodeTestTotal.Interfaces
{
    public interface IClientService
    {
        Task<bool> AddClient(int userID, string ClientName);
        List<Cliente> GetAllClients();
        Cliente GetClient(int UserID);
    }
}
