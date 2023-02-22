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
        private readonly IClientService _IClientService;
        public OrderController(IOrdenService IOrdenService, IPetService IPetService, IClientService IClientService)
        {
            _IOrdenService = IOrdenService;
            _IPetService = IPetService;
            _IClientService = IClientService;
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

        public async Task<ActionResult> MyOrders(int mascotaID, string nameMascota, string mascotaTipo)
        {
            List<MyOrdersViewModel> Model = new List<MyOrdersViewModel>();

            var orders = await _IOrdenService.GetPetsOrders(mascotaID);

            string foodImage = "";
            if (mascotaTipo == "P")
                foodImage = "ComidaPerro.png";
            else
                foodImage = "ComidaGato.png";

            foreach (var order in orders)
            {
                MyOrdersViewModel itemModel = new MyOrdersViewModel();
                itemModel.PetName = nameMascota;

                itemModel.PetFoodImage = foodImage;

                if (order.PedidoVendedorID != null)
                {
                    itemModel.OrderMessage = $"El pedido para Don Gato fue despachado con éxito a las {order.PedidoFechaDespachado} por el vendedor {order.PedidoVendedorNombre}";
                    itemModel.PetOrderProgressImage = "Despachado.png";
                }
                else
                {
                    itemModel.OrderMessage = "El pedido para Don Gato no fue despachado";
                    itemModel.PetOrderProgressImage = "NoDespachado.png";
                }
                Model.Add(itemModel);
            }

            return View(Model);
        }

        public async Task<ActionResult> ListOrders(int vendedorID = 0)
        {
            /*Get order history*/
            var orders = await _IOrdenService.GetAllOrders();

            if (vendedorID != 0)
                orders = orders.Where(x => x.PedidoVendedorID == vendedorID).ToList();

            List<ListOrdersViewModel> Model = new List<ListOrdersViewModel>();

            foreach (var oItem in orders)
            {
                //For each order... we get the pet and client
                ListOrdersViewModel oListOrdersViewModel = new ListOrdersViewModel();

                var oPet = _IPetService.GetPetByID(oItem.PedidoMascotaID);
                var client = _IClientService.GetClient(oPet.MascotaClientID);

                oListOrdersViewModel.PedidoID = oItem.PedidoID;
                oListOrdersViewModel.MascotaNombre = oPet.MascotaNombre;
                oListOrdersViewModel.ClienteNombre = client.ClienteNombre;
                oListOrdersViewModel.PedidoFecha = oItem.PedidoFecha;
                oListOrdersViewModel.PedidoFechaDespachado = oItem.PedidoFechaDespachado;
                oListOrdersViewModel.PedidoVendedorID = oItem.PedidoVendedorID;
                oListOrdersViewModel.PedidoVendedorNombre = oItem.PedidoVendedorNombre;

                Model.Add(oListOrdersViewModel);
            }

            return View(Model);
        }

        public async Task<ActionResult> MarkAsDespachado([FromBody]int PedidoID)
        {
            if(PedidoID <= 0)
            {
                return View();
            }

            var result = await _IOrdenService.MaskAsDespachado(PedidoID);

            if (result)
                return RedirectToAction("ListOrders", "Order");
            
            return View();
        }



    }
}
