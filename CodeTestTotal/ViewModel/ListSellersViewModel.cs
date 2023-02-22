namespace CodeTestTotal.ViewModel
{
    public class ListSellersViewModel
    {
        public int VendedorID { get; set; }
        public string VendedorNombre { get; set; }
        public string VendedorApellido { get; set; }
        public DateTime VendedorFechaIncorporación { get; set; }
        public int cantidadPedidosDespachadosDia { get; set; }
        public int cantidadPedidosDespachadosHistorial { get; set; }
    }
}
