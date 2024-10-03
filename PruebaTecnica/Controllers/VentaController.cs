using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PruebaTecnica.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult frmReporteVenta()
        {
            var listaVentas = clsGestionarWeb.Instancia.VentaListar();
            return View(listaVentas);
        }

        public IActionResult frmVenta()
        {
            var listaProductos = clsGestionarWeb.Instancia.ProductoListar();
            return View(listaProductos);
        }
        [HttpPost]
        [HttpPost]
        public JsonResult VentaInsertar(clsVenta objVentas, clsListaDetalleVenta objDetalleVentas)
        {
            // Comprobación de datos recibidos
            if (objVentas == null || objDetalleVentas == null || objDetalleVentas.Elementos == null)
            {
                return Json(new { respuesta = false, mensaje = "Datos no recibidos correctamente." });
            }

            // Crear datos de prueba (puedes eliminar esto después de las pruebas)
            objVentas = new clsVenta
            {
                IdVenta = 0,
                NumeroDocumento = "INV-001",
                RazonSocial = "Cliente de Prueba S.A.",
                Total = 150.75m,
                IdEstado = 1,
                FechaRegistro = DateTime.Now
            };

            objDetalleVentas = new clsListaDetalleVenta
            {
                Elementos = new List<clsDetalleVenta>
        {
            new clsDetalleVenta
            {
                IdProducto = 1,
                Precio = 75.25m,
                Cantidad = 2,
                Total = 150.50m,
                IdEstado = 1
            },
            new clsDetalleVenta
            {
                IdProducto = 2,
                Precio = 50.00m,
                Cantidad = 1,
                Total = 50.00m,
                IdEstado = 1
            }
        }
            };

            // Aquí llamamos a la función de inserción
            var ventaInsertar = clsGestionarWeb.Instancia.VentaInsertar(objVentas, objDetalleVentas);
            return Json(ventaInsertar);
        }

        public JsonResult ObtenerPrecioProducto(int id)
        {
            var producto = clsGestionarWeb.Instancia.ObtenerProducto(id); // Asegúrate de que este método devuelva el producto
            if (producto != null)
            {
                return Json(new { precio = producto.Precio }); // Ajusta según tu modelo
            }
            return Json(new { precio = 0 });
        }

    }
}
