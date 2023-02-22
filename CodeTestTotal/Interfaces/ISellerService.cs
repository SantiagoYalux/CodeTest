using CodeTestTotal.Models;

namespace CodeTestTotal.Interfaces
{
    public interface ISellerService
    {
        Task<List<Vendedor>> GetAllSellers();
    }
}
