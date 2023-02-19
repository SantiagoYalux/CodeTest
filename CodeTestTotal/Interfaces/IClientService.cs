using CodeTestTotal.Models;

namespace CodeTestTotal.Interfaces
{
    public interface IClientService
    {
        Cliente GetClient(int UserID);
    }
}
