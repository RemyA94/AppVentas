using CapaDatos.Interfaces;
using CapaEntidad;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Servicio
{
    public class RepositorioProductos : IRepositorioProductos
    {
        private readonly string _connectionString;
        public RepositorioProductos(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:DefaultConnection"];         
        }

        public List<Producto> Obtener() 
        {
            List<Producto> producto = new List<Producto>();
            string mensaje = string.Empty;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"Select p.IdProducto, p.Nombre, p.Descripcion,
                                m.IdMarca, m.Descripcion[DesMarca],
                                c.IdCategoria, c.Descripcion[DesCategiria],
                                p.Precio, p.Stock, p.NombreImagen, p.RutaImagen, p.Activo
                                from Producto p
                                inner join Marca m on m.IdMarca = p.IdMarca
                                inner join Categoria c on c.IdCategoria = p.IdCategoria";
                  
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            producto.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dataReader["IdProducto"]),
                                Nombre = dataReader["Nombre"].ToString(),
                                Descripcion = dataReader["Descripcion"].ToString(),
                                oMarca = new Marca() { IdMarca = Convert.ToInt32(dataReader["IdMarca"]), Descripcion = dataReader["DesMarca"].ToString()},
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]), Descripcion = dataReader["DesCategiria"].ToString()},
                                Precio = Convert.ToDecimal(dataReader["Precio"], new CultureInfo("en-US")),
                                Stock = Convert.ToInt32(dataReader["Stock"]),
                                RutaImagen = dataReader["RutaImagen"].ToString(),
                                NombreImagen = dataReader["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dataReader["Activo"])

                            });                             
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                producto = new List<Producto>();
                mensaje = ex.Message;

            }
            return producto;
        }

        public int Guardar(Producto producto,  out string Mensaje)
        {
            Mensaje = string.Empty;
            var idAutoGenerado = 0;

            try
            {
                using(var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_RegistrarProducto", connection);
                    cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", producto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", producto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("Activo", producto.Activo);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    idAutoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                idAutoGenerado = 0;
                Mensaje = ex.Message;
            }
            return idAutoGenerado;
        }


        public bool GuardarDatosImagen(Producto producto, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"Update Producto set RutaImagen = @RutaImagen,
                                NombreImagen = @NombreImagen where IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@RutaImagen", producto.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", producto.NombreImagen);

                    connection.Open();

                    if(cmd.ExecuteNonQuery() > 0) 
                    { resultado = true; }
                    else { Mensaje = "No se pudo actualizar imagen"; }
                }
            }
            catch(Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Editar(Producto producto, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_EditarProducto", connection);
                    cmd.Parameters.AddWithValue("IdProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", producto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", producto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("Activo", producto.Activo);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int  id, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_EliminarProducto", connection);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
