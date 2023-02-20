using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;
using System.Collections.Generic;

namespace CodeTestTotal.Services
{
    public class PetService : IPetService
    {
        private readonly DBContext _DbContext;
        public PetService(DBContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<Mascota> GetClientsPets(int clientID)
        {
            var mascotas = _DbContext.Mascotas;

            return mascotas.Where(x => x.MascotaClientID == clientID).ToList();
        }

        public async Task<bool> AddNewPetAsync(AddPetViewModel AddPetViewModel, int clientID)
        {
            bool value = false;
            var LastID = 1;

            Mascota oNewMascota = new Mascota();
            oNewMascota.MascotaID = LastID + 1;
            oNewMascota.MascotaNombre = AddPetViewModel.MascotaNombre;
            oNewMascota.MascotaClientID = clientID;
            oNewMascota.MascotaEdad = AddPetViewModel.MascotaEdad;
            oNewMascota.MascotaPeso = AddPetViewModel.MascotaPeso;
            
            if (AddPetViewModel.MascotaCastrado == "Si")
                oNewMascota.MascotaCastrado = true;
            else
                oNewMascota.MascotaCastrado = false;

            oNewMascota.MascotaImg = AddPetViewModel.MascotaImg;
            oNewMascota.MascotaTipo = AddPetViewModel.MascotaTipo;
            oNewMascota.MascotaDescrip = AddPetViewModel.MascotaDescrip;

            value = await _DbContext.AddNewRegister(oNewMascota);

            return value;
        }
    }
}
