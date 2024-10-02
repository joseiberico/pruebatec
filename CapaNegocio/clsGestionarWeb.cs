using CapaDatos;
using PruebaTecnica.Models;

namespace CapaNegocio
{
    public class clsGestionarWeb
    {
        // Llama al método para listar productos
        public List<clsProducto> ListarProductos()
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.ListarProductos();
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al listar productos: " + ex.Message);
            }
        }

        // Llama al método para crear un producto
        public bool CrearProducto(clsProducto producto)
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.CrearProducto(producto);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al crear producto: " + ex.Message);
            }
        }

        // Llama al método para actualizar un producto
        public bool ActualizarProducto(clsProducto producto)
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.ActualizarProducto(producto);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }

        // Llama al método para eliminar un producto
        public bool EliminarProducto(int idProducto)
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.EliminarProducto(idProducto);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }
    }
}
