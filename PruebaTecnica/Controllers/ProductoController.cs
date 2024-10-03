using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
namespace PruebaTecnica.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult frmProducto()
        {
            // Llamar a la capa de negocio para listar productos
            var listaProductos = clsGestionarWeb.Instancia.ProductoListar();

            // Enviar la lista de productos a la vista
            return View(listaProductos);
        }


        public IActionResult frmProductoInsertar()
        {
            return View();
        }

        // POST: frmProductoInsertar
        [HttpPost]
        public IActionResult frmProductoInsertar(clsProducto producto)
        {
            if (ModelState.IsValid)
            {
                // Aquí llamas a tu capa de negocio para insertar el producto
                clsGestionarWeb.Instancia.ProductoInsertar(producto);

                // Redirigir a la acción que lista los productos o a donde desees
                return RedirectToAction("frmProducto");
            }
            return View(producto); // Si hay errores, vuelve a mostrar la vista con el modelo
        }

        // GET: frmProductoEditar
        public IActionResult frmProductoEditar(int idProducto)
        {
            // Aquí debes recuperar el producto por su ID
            clsProducto producto = clsGestionarWeb.Instancia.ObtenerProducto(idProducto);
            if (producto == null)
            {
                return NotFound(); // Retornar 404 si el producto no se encuentra
            }
            return View(producto);
        }

        // POST: frmProductoEditar
        [HttpPost]
        public IActionResult frmProductoActualizar(clsProducto producto)
        {
            if (ModelState.IsValid)
            {
                // Aquí llamas a tu capa de negocio para actualizar el producto
                clsGestionarMaestroDao.Instancia.ProductoActualizar(producto);
                return RedirectToAction("frmProducto"); // Redirigir a la lista de productos
            }
            return View(producto); // Si hay errores, vuelve a mostrar la vista con el modelo
        }


        // POST: Confirmar eliminación
        [HttpPost]
        public IActionResult EliminarConfirmado(int id)
        {
            clsGestionarMaestroDao.Instancia.ProductoEliminar(id); // Llamar a la capa de negocio para eliminar el producto
            return RedirectToAction("frmProducto"); // Redirigir a la lista de productos
        }


    }
}
