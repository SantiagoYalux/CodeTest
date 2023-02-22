using CodeTestTotal.Interfaces;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestTotal.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerService _ISellerService;
        private readonly IOrdenService _IOrdenService;
        private readonly IUserService _IUserService;
        public SellerController(ISellerService ISellerService, IOrdenService IOrdenService, IUserService iUserService)
        {
            _ISellerService = ISellerService;
            _IOrdenService = IOrdenService;
            _IUserService = iUserService;
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

        public async Task<ActionResult> _AddSeller()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> _AddSeller(AddSellerViewModel newSeller)
        {
            if (!ModelState.IsValid)
            {
                return View(newSeller);
            }
            /*If model is valid, add the new seller*/
            //1- new user
            var newUser = await _IUserService.AddUser(newSeller.VendedorUsername, newSeller.VendedorPassword);

            if (newUser == 0)
            {
                ModelState.AddModelError("", "Problemas al agregar el usuario");
                return View(newSeller);
            }

            //2- new seller
            var result = await _ISellerService.AddNewSeller(newSeller,newUser);

            if (result)
                return RedirectToAction("ListSellers","Seller");

            return View(newSeller);
        }
    }
}
