using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.Services;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTestTotal.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _IPetService;
        private readonly IUserService _IUserService;
        private UserManager<Usuario> _UserManager;
        private IClientService _IClientService;
        public PetController(IPetService IPetService, IUserService iUserService, UserManager<Usuario> UserManager, IClientService IClientService)
        {
            _IPetService = IPetService;
            _IUserService = iUserService;
            _UserManager = UserManager;
            _IClientService = IClientService;
        }
        public async Task<IActionResult> AddPetAsync()
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Cliente");

            if (roles == true)
            {
                List<SelectListItem> lstTipoMascota = new List<SelectListItem>();
                lstTipoMascota.Add(new SelectListItem() { Text = "Perro", Value = "P" });
                lstTipoMascota.Add(new SelectListItem() { Text = "Gato", Value = "G" });

                List<SelectListItem> lstCastrado = new List<SelectListItem>();
                lstCastrado.Add(new SelectListItem() { Text = "Si", Value = "Si" });
                lstCastrado.Add(new SelectListItem() { Text = "No", Value = "No" });

                ViewBag.OpcionesTipoMascota = lstTipoMascota;
                ViewBag.OpcionesCastrado = lstCastrado;
                return PartialView();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddPetAsync(AddPetViewModel NewPet)
        {
            var claims = User.Claims.ToList();
            var userID = int.Parse(claims[0].Value);

            var user = await _IUserService.GetUserById(userID);
            var roles = await _UserManager.IsInRoleAsync(user, "Cliente");

            if (roles == true)
            {
                if (!ModelState.IsValid)
                {
                    List<SelectListItem> lstTipoMascota = new List<SelectListItem>();
                    lstTipoMascota.Add(new SelectListItem() { Text = "Perro", Value = "P" });
                    lstTipoMascota.Add(new SelectListItem() { Text = "Gato", Value = "G" });

                    List<SelectListItem> lstCastrado = new List<SelectListItem>();
                    lstCastrado.Add(new SelectListItem() { Text = "Si", Value = "Si" });
                    lstCastrado.Add(new SelectListItem() { Text = "No", Value = "No" });

                    ViewBag.OpcionesTipoMascota = lstTipoMascota;
                    ViewBag.OpcionesCastrado = lstCastrado;
                    return View(NewPet);
                }

                var client = _IClientService.GetClient(userID);
                if(client == null)
                {
                    ModelState.AddModelError(string.Empty, "El usuario no tiene relacionado un cliente, no podrá realizar ésta operación");
                    return View(NewPet);
                }

                if (await _IPetService.AddNewPetAsync(NewPet, client.ClienteID))
                {
                    return RedirectToAction("Index", "Client");
                }

                ModelState.AddModelError(string.Empty, "Error al generar mascota");
                return View(NewPet);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
    }
}
