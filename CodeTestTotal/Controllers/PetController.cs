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

            ViewBag.OpcionesTipoMascota = lstTipoMascota;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPetAsync(AddPetViewModel NewPet)
        {

            if (await _IPetService.AddNewPetAsync(NewPet, 1))
            {
                RedirectToAction("Index", "Client");
            }

            return View();
        }
    }
}
