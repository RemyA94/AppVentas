using CapaDatos.Interfaces;
using CapaEntidad;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace CapaDatos.Servicio
{
    public class RepositorioMarcas : IRepositorioMarcas
    {
        private readonly string _connectionString;
        public RepositorioMarcas(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:DefaultConnection"];
        }

        public async Task<IEnumerable<Marca>> Obtener()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Marca>(@"Select IdMarca, Descripcion, Activo from Marca");
        }

        public int Guardar(Marca marca, out string Mensaje)
        {
            Mensaje = string.Empty;
            var idAutoGenerado = 0;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_RegistrarMarca", connection);
                    cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", marca.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    idAutoGenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
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

        public bool Editar(Marca marca, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using(var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_EditarMarca", connection);
                    cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", marca.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            Mensaje=string.Empty;
            bool resultado = false;

            try
            {
                using(var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_EliminarMarca", connection);
                    cmd.Parameters.AddWithValue("IdMarca", id);
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
            catch(Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
