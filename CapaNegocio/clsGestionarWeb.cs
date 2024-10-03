using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class clsGestionarWeb
    {
        // Instancia única de la clase
        private static readonly clsGestionarWeb _instancia = new clsGestionarWeb();

        // Propiedad de acceso a la instancia
        public static clsGestionarWeb Instancia => _instancia;

        public List<clsProducto> ProductoListar()
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.ProductoListar();
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al listar productos: " + ex.Message);
            }
        }

        // Llama al método para crear un producto
        public bool ProductoInsertar(clsProducto producto)
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.ProductoInsertar(producto);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al crear producto: " + ex.Message);
            }
        }

        public clsProducto ObtenerProducto(int idProducto)
        {
            try
            {
                // Llama al método de la capa de datos
                return clsGestionarMaestroDao.Instancia.ObtenerProductoPorId(idProducto);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener el producto: " + ex.Message);
            }
        }

        // Llama al método para actualizar un producto
        public bool ProductoActualizar(clsProducto producto)
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.ProductoActualizar(producto);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }

        // Llama al método para eliminar un producto
        public bool ProductoEliminar(int idProducto)
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.ProductoEliminar(idProducto);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }

        // Llama al método para eliminar un producto
        public List<clsVenta> VentaListar()
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.VentaListar();
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al listar productos: " + ex.Message);
            }
        }

        // Llama al método para actualizar un producto
        public bool VentaInsertar(clsVenta objVenta, clsListaDetalleVenta objDetalleVenta)
        {
            try
            {
                return clsGestionarMaestroDao.Instancia.VentaInsertar(objVenta,objDetalleVenta);
            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o lanzar una excepción
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }
    }
}
