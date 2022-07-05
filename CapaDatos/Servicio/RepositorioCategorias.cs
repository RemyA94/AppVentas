using CapaDatos.Interfaces;
using CapaEntidad;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace CapaDatos.Servicio
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly string connectionString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Categoria>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Categoria>(
                @"select IdCategoria, Descripcion, 
                Activo from Categoria");
        }

        public int Guardar(Categoria categoria, out string Mensaje)
        {
            Mensaje = string.Empty;
            var idAutoGenerado = 0;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_RegistrarCategoria", connection);
                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", categoria.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    idAutoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idAutoGenerado = 0;
                Mensaje = ex.Message;
            }
            return idAutoGenerado;
        }

        public bool Editar(Categoria categoria, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_EditarCategoria", connection);
                    cmd.Parameters.AddWithValue("IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", categoria.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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

        public bool Eliminar(int id, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_EliminarCategoria", connection);
                    cmd.Parameters.AddWithValue("IdCategoria", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output; 
                    cmd.CommandType = CommandType.Text;
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
