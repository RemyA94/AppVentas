using CapaDatos.Interfaces;
using CapaEntidad;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CapaDatos.Servicio
{

    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly string connectionString;
        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public async Task<IEnumerable<Usuario>> Obtener()
        {
            using var connetion = new SqlConnection(connectionString);

            return await connetion.QueryAsync<Usuario>(
                @"select IdUsuario, Nombre, Apellido, 
                Correo, Clave, Activo from Usuario");

        }

        public int Guardar(Usuario usuario, out string Mensaje)
        {
            Mensaje = String.Empty;
            var idautogenerado = 0;
  
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_RegistrarUsuarios", connection);
                    cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("Activo", usuario.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;


                    connection.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;

        }

        public bool Editar(Usuario usuario, out string Mensaje)
        {
            Mensaje = String.Empty;           
            bool resultado= false;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"sp_EditarUsuarios", connection);
                    cmd.Parameters.AddWithValue("IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("Activo", usuario.Activo);
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
                using(var connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"Delete top (1) from Usuario where id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
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
