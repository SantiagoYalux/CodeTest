using CodeTestTotal.ViewModel;

namespace CodeTestTotal.Interfaces
{
    public interface IOrdenService
    {
        Task<bool> AddNewOrderAsync(NewOrdenViewModel oNewOrderViewModel);
    }
}
