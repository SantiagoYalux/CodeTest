using CodeTestTotal.Interfaces;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestTotal.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerService _ISellerService;
        private readonly IOrdenService _IOrdenService;
        public SellerController(ISellerService ISellerService, IOrdenService IOrdenService)
        {
            _ISellerService = ISellerService;
            _IOrdenService = IOrdenService;
        }
        public async Task<ActionResult> ListSellers()
        {
            List<ListSellersViewModel> Model = new List<ListSellersViewModel>();

            var listSellers = await _ISellerService.GetAllSellers();


            foreach (var seller in listSellers)
            {
                ListSellersViewModel oItem = new ListSellersViewModel();
                oItem.VendedorID = seller.VendedorID;
                oItem.VendedorNombre = seller.VendedorNombre;
                oItem.VendedorApellido = seller.VendedorApellido;
                oItem.VendedorFechaIncorporación = seller.VendedorFechaIncorporación;

                int despachadosNow = await _IOrdenService.GetNumberOfOrders(seller.VendedorID, true);
                oItem.cantidadPedidosDespachadosDia = despachadosNow;

                int despachadosHistory = await _IOrdenService.GetNumberOfOrders(seller.VendedorID, false);
                oItem.cantidadPedidosDespachadosHistorial = despachadosHistory;

                Model.Add(oItem);
            }

            return View(Model);
        }
    }
}
