using CapaEntidad;
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

        public List<clsProducto> ProductoListar()
        {
            var oLista = new List<clsProducto>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("Producto_Listar_sp", conexion))
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
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                IdEstado = Convert.ToInt16(dr["IdEstado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]) // Convertir y asignar la fecha
                            });
                        }
                    }
                }
            }

            return oLista;
        }

        public clsProducto ObtenerProductoPorId(int idProducto)
        {
            clsProducto producto = null;
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("Producto_SeleccionarPorId_sp", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto); // Parámetro

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Verificar si se encontró un registro
                        {
                            producto = new clsProducto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                IdEstado = Convert.ToInt16(dr["IdEstado"])
                            };
                        }
                    }
                }
            }

            return producto;
        }

        public bool ProductoInsertar(clsProducto producto)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("Producto_Insertar_sp", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@p_precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@p_stock", producto.Stock);

                    return cmd.ExecuteNonQuery() > 0; // Retorna true si se inserta con éxito
                }
            }
        }

        public bool ProductoActualizar(clsProducto producto)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("Producto_Actualizar_sp", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_idproducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@p_nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@p_precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@p_stock", producto.Stock);

                    return cmd.ExecuteNonQuery() > 0; // Retorna true si se actualiza con éxito
                }
            }
        }

        public bool ProductoEliminar(int idProducto)
        {
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("Producto_Eliminar_sp", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_idproducto", idProducto);

                    return cmd.ExecuteNonQuery() > 0; // Retorna true si se elimina con éxito
                }
            }
        }

        public List<clsVenta> VentaListar()
        {
            var oLista = new List<clsVenta>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("Venta_Listar_sp", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new clsVenta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                Total = Convert.ToDecimal(dr["Total"]),
                                IdEstado = Convert.ToInt16(dr["IdEstado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]) // Convertir y asignar la fecha
                            });
                        }
                    }
                }
            }

            return oLista;
        }


        public bool VentaInsertar(clsVenta objVenta, clsListaDetalleVenta objDetalleVenta)
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
                        using (SqlCommand cmd = new SqlCommand("Venta_Insertar_sp", sqlConnection, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Definir parámetros
                            SqlParameter salidaIdVenta = new SqlParameter("@p_idventa", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(salidaIdVenta);
                            cmd.Parameters.AddWithValue("@p_numeroDocumento", objVenta.NumeroDocumento);
                            cmd.Parameters.AddWithValue("@p_razonSocial", objVenta.RazonSocial);
                            cmd.Parameters.AddWithValue("@p_total", objVenta.Total);
                            cmd.Parameters.AddWithValue("@p_idestado", objVenta.IdEstado);

                            cmd.ExecuteNonQuery();

                            // Obtener el ID de venta generado
                            objVenta.IdVenta = Convert.ToInt32(salidaIdVenta.Value);
                        }

                        // Insertar los detalles de la venta
                        foreach (var detalle in objDetalleVenta.Elementos)
                        {
                            using (SqlCommand cmdDetalle = new SqlCommand("DetalleVenta_Insertar_sp", sqlConnection, tran))
                            {
                                cmdDetalle.CommandType = CommandType.StoredProcedure;

                                cmdDetalle.Parameters.AddWithValue("@p_idventa", objVenta.IdVenta);
                                cmdDetalle.Parameters.AddWithValue("@p_idproducto", detalle.IdProducto);
                                cmdDetalle.Parameters.AddWithValue("@p_precio", detalle.Precio);
                                cmdDetalle.Parameters.AddWithValue("@p_cantidad", detalle.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@p_total", detalle.Total);
                                cmdDetalle.Parameters.AddWithValue("@p_idestado", detalle.IdEstado);

                                cmdDetalle.ExecuteNonQuery();
                            }
                        }

                        // Confirmar la transacción
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine("Error al insertar la venta: " + ex.Message);
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
