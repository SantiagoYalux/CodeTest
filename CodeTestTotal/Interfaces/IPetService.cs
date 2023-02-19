using CodeTestTotal.Models;

namespace CodeTestTotal.Interfaces
{
    public interface IPetService
    {
        List<Mascota> GetClientsPets(int clientID);
    }
}
