using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestTotal.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerService _ISellerService;
        private readonly IOrdenService _IOrdenService;
        private readonly IUserService _IUserService;
        private UserManager<Usuario> _UserManager;
        public SellerController(ISellerService ISellerService, IOrdenService IOrdenService, IUserService iUserService, UserManager<Usuario> UserManager)
        {
            _ISellerService = ISellerService;
            _IOrdenService = IOrdenService;
            _IUserService = iUserService;
            _UserManager = UserManager;
        }
        [HttpGet]
        public async Task<ActionResult> ListSellers()
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Vendedor");
           

            if (roles == true)
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
            else
                return RedirectToAction("Login", "User");
        }

        public async Task<ActionResult> _AddSeller()
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Vendedor");


            if (roles == true)
            {
                return PartialView();
            }
            else
                return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public async Task<ActionResult> _AddSeller(AddSellerViewModel newSeller)
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Vendedor");
            if (roles == true)
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
                var result = await _ISellerService.AddNewSeller(newSeller, newUser);

                if (result)
                    return RedirectToAction("ListSellers", "Seller");

                return View(newSeller);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}
