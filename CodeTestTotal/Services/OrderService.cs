using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;

namespace CodeTestTotal.Services
{
    public class OrderService : IOrdenService
    {
        private readonly DBContext _DbContext;
        public OrderService(DBContext DBContext)
        {
            _DbContext = DBContext;
        }

        public async Task<bool> AddNewOrderAsync(NewOrdenViewModel oNewOrderViewModel)
        {
            bool result = false;

            Pedido oNewPedido = new Pedido();
            oNewPedido.PedidoID = 1;

            oNewPedido.PedidoMascotaID = oNewOrderViewModel.MascotaID;

            oNewPedido.PedidoCantidadAlimiento = oNewOrderViewModel.CantidadAlimiento;

            oNewPedido.PedidoComplementoAlimientoEdad = oNewOrderViewModel.CantComplementoAlimientoEdad;

            oNewPedido.PedidoComplementoAlimientoCastrado = oNewOrderViewModel.CantComplementoAlimientoCastrado;

            oNewPedido.PedidoFecha = DateTime.Now;
            oNewPedido.PedidoFechaDespachado = DateTime.Now;
            /*when registering the new order the following fields will not be filled*/
            oNewPedido.PedidoDespachado = null;
            oNewPedido.PedidoVendedorID = null;

            result = await _DbContext.AddNewRegister(oNewPedido);

            return result;
        }

        public async Task<List<Pedido>> GetPetsOrders(int mascotaID)
        {
            var Pedidos = _DbContext.Pedidos;

            return Pedidos.Where(x => x.PedidoMascotaID == mascotaID).OrderByDescending(x => x.PedidoFecha).ToList();
        }
        public async Task<List<Pedido>> GetAllOrders()
        {
            var Pedidos = _DbContext.Pedidos;

            return Pedidos;
        }

        public async Task<bool> MaskAsDespachado(int PedidoID)
        {
            bool result = false;

            var pedido = _DbContext.Pedidos.FirstOrDefault(x => x.PedidoID == PedidoID);

            if (pedido != null)
            {
                pedido.PedidoFechaDespachado = DateTime.Now;
                pedido.PedidoDespachado = true;
                pedido.PedidoVendedorID = 1;
                pedido.PedidoVendedorNombre = "Santiago";
                result = await _DbContext.ModRegister(pedido);
            }

            return result;
        }

        //public async Task<int> GetCountOrderByClientID(int clientID)
        //{
        //    var Pedidos = _DbContext.Pedidos;

        //}
    }
}
