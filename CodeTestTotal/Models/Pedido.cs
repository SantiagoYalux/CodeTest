namespace CodeTestTotal.Models
{
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int PedidoMascotaID { get; set; }
        public double PedidoCantidadAlimiento { get; set; }
        public int PedidoComplementoAlimientoEdad { get; set; }
        public int PedidoComplementoAlimientoCastrado { get; set; }
        public DateTime PedidoFecha { get; set; }
        public bool? PedidoDespachado { get; set; }
        public DateTime? PedidoFechaDespachado { get; set; }
        public int? PedidoVendedorID { get; set; }
        public string? PedidoVendedorNombre { get; set; }
    }
}
