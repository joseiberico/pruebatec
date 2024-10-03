using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class clsProducto
    {
        public Int32 IdProducto { get; set; }        // Identificador único del producto
        public string Nombre { get; set; }            // Nombre del producto
        public decimal Precio { get; set; }           // Precio del producto
        public Int32 Stock { get; set; }              // Cantidad en stock del producto
        public Int16 IdEstado { get; set; }           // Estado del producto (activo/inactivo)
        public DateTime FechaRegistro { get; set; } // Fecha en que se registró el producto
    }
}
