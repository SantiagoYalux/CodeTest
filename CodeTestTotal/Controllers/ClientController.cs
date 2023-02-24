using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.Services;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeTestTotal.Controllers
{
    public class ClientController : Controller
    {
        private IPetService _IPetService;
        private IClientService _IClientService;
        private readonly IUserService _IUserService;
        private UserManager<Usuario> _UserManager;
        public ClientController(IPetService IPetService, IClientService IClientService, IUserService iUserService, UserManager<Usuario> UserManager)
        {
            _IPetService = IPetService;
            _IClientService = IClientService;
            _IUserService = iUserService;
            _UserManager = UserManager;
        }

        public async Task<IActionResult> Index()
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Cliente");

            if (roles == true)
            {

                var client = _IClientService.GetClient(userID);

                if (client == null)
                {
                    return RedirectToAction("Login", "User");
                }

                var mascotas = _IPetService.GetClientPets(client.ClienteID);
                return View(mascotas);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public async Task<ActionResult> ListClientPets()
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Vendedor");

            if (roles == true)
            {
                List<ListClientPetsViewModel> Model = new List<ListClientPetsViewModel>();
                /*Get All clients*/
                var Clients = _IClientService.GetAllClients();


                foreach (var Client in Clients)
                {
                    ListClientPetsViewModel oItem = new ListClientPetsViewModel();

                    oItem.clientID = Client.ClienteID;
                    oItem.clientName = Client.ClienteNombre;

                    var petsClient = _IPetService.GetClientPets(Client.ClienteID);
                    oItem.pets = petsClient;

                    oItem.cantOrder = 5;

                    Model.Add(oItem);
                }

                return View(Model);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        public async Task<ActionResult> _ListPets(int clientID)
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Vendedor");

            if (roles == true)
            {
                var model = _IPetService.GetClientPets(clientID);
                return PartialView(model);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}
