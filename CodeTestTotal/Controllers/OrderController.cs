using CodeTestTotal.Interfaces;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Concurrent;

namespace CodeTestTotal.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdenService _IOrdenService;
        private readonly IPetService _IPetService;
        public OrderController(IOrdenService IOrdenService, IPetService IPetService)
        {
            _IOrdenService = IOrdenService;
            _IPetService = IPetService;
        }

        public IActionResult NewOrder(int mascotaID, string nameMascota)
        {
            /*Logic for pets combo*/
            /*Para gatos:
                ● Cantidad de alimento igual a 0.5 veces su peso.
                ● Un complemento dietario si el gato tiene más de 5 años.
                ● Un complemento dietario extra si el gato está castrado.
            Para perros:
                ● Cantidad de alimento igual a 0.8 veces su peso.
                ● Un complemento dietario por cada 3 años edad.
                ● Un complemento dietario extra si el perro está castrado y tiene más de 5 años.*/

            var pet = _IPetService.GetPetByID(mascotaID);

            /*model*/
            NewOrdenViewModel Model = new NewOrdenViewModel();
            double alimento = 0;
            int complementoDietario = 0;
            int complementoDietarioExtra = 0;

            if (pet.MascotaTipo == "G")
            {
                alimento = pet.MascotaPeso * 0.5;
                if (pet.MascotaEdad > 5)
                    complementoDietario = 1;

                if (pet.MascotaCastrado == true)
                    complementoDietarioExtra = 1;

            }
            else
            {
                alimento = pet.MascotaPeso * 0.8;

                int v = pet.MascotaEdad / 3;
                complementoDietario = 1 * int.Parse(v.ToString());

                if (pet.MascotaCastrado == true && pet.MascotaEdad > 5)
                    complementoDietarioExtra = 1;
            }

            Model.CantidadAlimiento = alimento;
            Model.CantComplementoAlimientoEdad = complementoDietario;
            Model.CantComplementoAlimientoCastrado = complementoDietarioExtra;
            Model.MascotaID = mascotaID;
            Model.MascotaNombre = nameMascota;
            Model.MascotaPeso = pet.MascotaPeso;
            Model.MascotaEdad = pet.MascotaEdad;
            Model.MascotaCastrado = pet.MascotaCastrado;

            return View(Model);
        }

        [HttpPost]
        public async Task<ActionResult> NewOrder(NewOrdenViewModel oNewOrdenViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(oNewOrdenViewModel);
            }

            var result = await _IOrdenService.AddNewOrderAsync(oNewOrdenViewModel);

            if (result)
            {
                return RedirectToAction("Index", "Client");
            }

            ModelState.AddModelError("", "Errores al intentar agregar la orden");
            return View(oNewOrdenViewModel);
        }
    }
}
