using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;

namespace CodeTestTotal.Interfaces
{
    public interface ISellerService
    {
        Task<bool> AddNewSeller(AddSellerViewModel oNewSellerViewModel, int newUserID);
        Task<List<Vendedor>> GetAllSellers();
        Task<Vendedor> GetSellerByUserID(int userID);
    }
}
