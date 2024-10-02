using PruebaTecnica.Datos;
using PruebaTecnica.Models;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class clsGestionarMaestroDao
    {
        // Instancia única de la clase
        private static readonly clsGestionarMaestroDao _instancia = new clsGestionarMaestroDao();

        // Propiedad de acceso a la instancia
        public static clsGestionarMaestroDao Instancia => _instancia;


        public List<clsProducto> ListarProductos()
        {
            var oLista = new List<clsProducto>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("sp_ListarProductos", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new clsProducto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                IdEstado = Convert.ToInt16(dr["IdEstado"])
                            });
                        }
                    }
                }
            }

            return oLista;
        }

        public bool CrearProducto(clsProducto producto)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("sp_CrearProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@p_descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@p_precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@p_stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@p_idestado", producto.IdEstado);

                    return cmd.ExecuteNonQuery() > 0; // Retorna true si se inserta con éxito
                }
            }
        }

        public bool ActualizarProducto(clsProducto producto)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("sp_ActualizarProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_idproducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@p_nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@p_descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@p_precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@p_stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@p_idestado", producto.IdEstado);

                    return cmd.ExecuteNonQuery() > 0; // Retorna true si se actualiza con éxito
                }
            }
        }

        public bool EliminarProducto(int idProducto)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("sp_EliminarProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_idproducto", idProducto);

                    return cmd.ExecuteNonQuery() > 0; // Retorna true si se elimina con éxito
                }
            }
        }


        public bool InsertarVenta(clsVenta objVenta, clsListaDetalleVenta objDetalleVenta)
        {
            var cn = new Conexion();

            using (SqlConnection sqlConnection = new SqlConnection(cn.getCadenaSQL()))
            {
                sqlConnection.Open();

                using (SqlTransaction tran = sqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Insertar la venta
                        using (SqlCommand cmd = new SqlCommand("Venta_Insertar_pa", sqlConnection, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@p_idventa", objVenta.IdVenta); // Salida
                            cmd.Parameters.AddWithValue("@p_numeroDocumento", objVenta.NumeroDocumento);
                            cmd.Parameters.AddWithValue("@p_razonSocial", objVenta.RazonSocial);
                            cmd.Parameters.AddWithValue("@p_total", objVenta.Total);
                            cmd.Parameters.AddWithValue("@p_idestado", objVenta.IdEstado); // Nuevo parámetro IdEstado

                            // Asignar un parámetro de salida
                            SqlParameter salidaIdVenta = new SqlParameter("@p_idventa", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(salidaIdVenta);

                            cmd.ExecuteNonQuery();

                            // Obtener el valor del parámetro de salida (IdVenta)
                            objVenta.IdVenta = Convert.ToInt32(salidaIdVenta.Value);
                        }

                        // Insertar los detalles de la venta
                        foreach (var detalle in objDetalleVenta.Elementos)
                        {
                            using (SqlCommand cmdDetalle = new SqlCommand("DetalleVenta_Insertar_pa", sqlConnection, tran))
                            {
                                cmdDetalle.CommandType = CommandType.StoredProcedure;

                                cmdDetalle.Parameters.AddWithValue("@p_idventa", objVenta.IdVenta);
                                cmdDetalle.Parameters.AddWithValue("@p_idproducto", detalle.IdProducto);
                                cmdDetalle.Parameters.AddWithValue("@p_precio", detalle.Precio);
                                cmdDetalle.Parameters.AddWithValue("@p_cantidad", detalle.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@p_total", detalle.Total);
                                cmdDetalle.Parameters.AddWithValue("@p_idestado", detalle.IdEstado); // Nuevo parámetro IdEstado

                                cmdDetalle.ExecuteNonQuery();
                            }
                        }

                        // Confirmar la transacción si todo va bien
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Mejorar el manejo de errores
                        Console.WriteLine("Error al insertar la venta: " + ex.Message);
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
