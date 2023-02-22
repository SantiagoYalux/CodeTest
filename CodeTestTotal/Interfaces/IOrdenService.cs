using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;

namespace CodeTestTotal.Interfaces
{
    public interface IOrdenService
    {
        Task<bool> AddNewOrderAsync(NewOrdenViewModel oNewOrderViewModel);
        Task<List<Pedido>> GetAllOrders();
        Task<int> GetNumberOfOrders(int VendedorID, bool onlyToday);
        Task<List<Pedido>> GetPetsOrders(int mascotaID);
        Task<bool> MaskAsDespachado(int PedidoID);
    }
}
