using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class clsDetalleVenta
    {
        public Int32 IdDetalleVenta { get; set; }
        public Int32 IdVenta { get; set; }
        public Int32 IdProducto { get; set; } // Ahora se usa IdProducto
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public Int16 IdEstado { get; set; } // Nuevo parámetro IdEstado


    }

    public class clsListaDetalleVenta
    {
        // Propiedad que almacena la lista de detalles de venta
        public List<clsDetalleVenta> Elementos { get; set; } = new List<clsDetalleVenta>();
    }
}
