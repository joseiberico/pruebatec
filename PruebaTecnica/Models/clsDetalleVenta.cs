namespace PruebaTecnica.Models
{
    public class clsDetalleVenta
    {
        public Int32 IdProducto { get; set; } // Ahora se usa IdProducto
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public Int16 IdEstado { get; set; } // Nuevo parámetro IdEstado

        
    }

    public class clsListaDetalleVenta
    {
        // Propiedad la lista de detalles de venta
        public List<clsDetalleVenta> Elementos { get; set; } = new List<clsDetalleVenta>();
    }
}
