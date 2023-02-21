namespace CodeTestTotal.ViewModel
{
    public class ListOrdersViewModel
    {
        public int PedidoID { get; set; }
        public string? MascotaNombre { get; set; }
        public string? ClienteNombre { get; set; }
        public DateTime PedidoFecha { get; set; }
        public DateTime? PedidoFechaDespachado { get; set; }
        public int? PedidoVendedorID { get; set; }
        public string? PedidoVendedorNombre { get; set; }
    }
}
