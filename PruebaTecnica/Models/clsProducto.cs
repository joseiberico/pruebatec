namespace PruebaTecnica.Models
{
    public class clsProducto
    {
        public Int32 IdProducto { get; set; }        // Identificador único del producto
        public string Nombre { get; set; }            // Nombre del producto
        public string Descripcion { get; set; }       // Descripción del producto
        public decimal Precio { get; set; }           // Precio del producto
        public Int32 Stock { get; set; }              // Cantidad en stock del producto
        public Int16 IdEstado { get; set; }           // Estado del producto (activo/inactivo)
    }
}
