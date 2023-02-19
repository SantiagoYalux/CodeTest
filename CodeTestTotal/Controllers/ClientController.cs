using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeTestTotal.Controllers
{
    public class ClientController : Controller
    {
        private IPetService _IPetService;
        public ClientController(IPetService IPetService)
        {
            _IPetService = IPetService;
        }

        public async Task<IActionResult> Index()
        {
            var mascotas = _IPetService.GetClientsPets(1);
            return View(mascotas);
        }
    }
}
