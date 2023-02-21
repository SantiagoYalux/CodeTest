using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;

namespace CodeTestTotal.Interfaces
{
    public interface IPetService
    {
        Task<bool> AddNewPetAsync(AddPetViewModel AddPetViewModel, int clientID);
        List<Mascota> GetAllPets();
        List<Mascota> GetClientPets(int clientID);
        Mascota GetPetByID(int MascotaID);
    }
}
