using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeTestTotal.Controllers
{
    public class ClientController : Controller
    {
        private IPetService _IPetService;
        private IClientService _IClientService;
        public ClientController(IPetService IPetService, IClientService IClientService)
        {
            _IPetService = IPetService;
            _IClientService = IClientService;
        }

        public async Task<IActionResult> Index()
        {
            var mascotas = _IPetService.GetClientPets(1);
            return View(mascotas);
        }

        public async Task<ActionResult> ListClientPets()
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

        public async Task<ActionResult> _ListPets(int clientID)
        {
            var model = _IPetService.GetClientPets(clientID);
            return PartialView(model);
        }
    }
}
