using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;

namespace CodeTestTotal.Services
{
    public class SellerService : ISellerService
    {
        private readonly DBContext _DbContext;
        public SellerService(DBContext DBContext)
        {
            _DbContext = DBContext;
        }

        public async Task<List<Vendedor>> GetAllSellers()
        {
            return _DbContext.Vendedores.ToList();
        }
    }
}
