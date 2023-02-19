using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
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
            
            return mascotas.Where(x=>x.MascotaClientID== clientID).ToList();
        }
    }
}
