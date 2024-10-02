namespace PruebaTecnica.Models
{
    public class clsVenta
    {
        public Int32 IdVenta { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public decimal Total { get; set; }
        public Int16 IdEstado { get; set; } // Nuevo parámetro IdEstado
        public List<clsDetalleVenta> Detalles { get; set; } = new List<clsDetalleVenta>(); // Lista de detalles de venta
    }
}
