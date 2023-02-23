using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;

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
        public async Task<bool> AddNewSeller(AddSellerViewModel oNewSellerViewModel, int newUserID)
        {
            bool result = false;

            Vendedor oNewVendedor = new Vendedor();
            oNewVendedor.VendedorID = await _DbContext.GetLastId(oNewVendedor) +1;
            oNewVendedor.VendedorUsuarioID = newUserID;

            oNewVendedor.VendedorNombre = oNewSellerViewModel.VendedorNombre;

            oNewVendedor.VendedorApellido = oNewSellerViewModel.VendedorApellido;

            oNewVendedor.VendedorFechaIncorporación = oNewSellerViewModel.VendedorFechaIncorporación;

            oNewVendedor.VendedorUsuarioID = newUserID;

            result = await _DbContext.AddNewRegister(oNewVendedor);

            return result;
        }

    }
}
