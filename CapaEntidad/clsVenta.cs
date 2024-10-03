using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class clsVenta
    {
        public Int32 IdVenta { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public decimal Total { get; set; }
        public Int16 IdEstado { get; set; } // Nuevo parámetro IdEstado
        public DateTime FechaRegistro { get; set; } // Fecha en que se registró el producto
        //public List<clsDetalleVenta> Detalles { get; set; } = new List<clsDetalleVenta>(); // Lista de detalles de venta
    }
}
