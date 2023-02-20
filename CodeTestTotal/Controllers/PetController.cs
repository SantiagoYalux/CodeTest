using CodeTestTotal.Interfaces;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTestTotal.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _IPetService;
        public PetController(IPetService IPetService)
        {
            _IPetService = IPetService;
        }
        public IActionResult AddPet()
        {
            List<SelectListItem> lstTipoMascota = new List<SelectListItem>();
            lstTipoMascota.Add(new SelectListItem() { Text = "Perro", Value = "P" });
            lstTipoMascota.Add(new SelectListItem() { Text = "Gato", Value = "G" });

            List<SelectListItem> lstCastrado = new List<SelectListItem>();
            lstCastrado.Add(new SelectListItem() { Text = "Si", Value = "Si" });
            lstCastrado.Add(new SelectListItem() { Text = "No", Value = "No" });

            ViewBag.OpcionesTipoMascota = lstTipoMascota;
            ViewBag.OpcionesCastrado = lstCastrado;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPetAsync(AddPetViewModel NewPet)
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


            if (await _IPetService.AddNewPetAsync(NewPet, 1))
            {
                RedirectToAction("Index", "Client");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al generar mascota");
                return View(NewPet);
            }

        }
    }
}
