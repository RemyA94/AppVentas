using CapaDatos.Interfaces;
using CapaEntidad;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


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

        public async Task<int> Guardar(Usuario usuario, string mensaje)
        {
            var resultado = 0;
            mensaje = string.Empty;
            var parametros = new DynamicParameters();
            parametros.Add("Nombre", usuario.Nombre);
            parametros.Add("Apellido", usuario.Apellido);
            parametros.Add("Correo", usuario.Correo);
            parametros.Add("Clave", usuario.Clave);
            parametros.Add("Activo", usuario.Activo);
            parametros.Add("Mensaje", dbType: System.Data.DbType.String,
                direction: System.Data.ParameterDirection.Output);
            parametros.Add("Resultado", dbType: System.Data.DbType.Int32, 
                direction: System.Data.ParameterDirection.Output, size: 500);
            try
            {
                using var connetion = new SqlConnection(connectionString);
                await connetion.ExecuteAsync(@"sp_RegistrarUsuarios", parametros,
                                             commandType: System.Data.CommandType.StoredProcedure);

                mensaje = parametros.Get<string>("Mensaje");
                resultado = parametros.Get<int>("Resultado");
            }
            catch (Exception ex)
            {
                resultado = 0;
               Console.WriteLine(ex.Message);
            }
            return Convert.ToInt32(resultado); 
        }

         public async Task<bool> Editar(Usuario usuario, string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            var parametros = new DynamicParameters();
            parametros.Add("IdUsuario",usuario.IdUsuario);
            parametros.Add("Nombre",usuario.Nombre);
            parametros.Add("Apellido", usuario.Apellido);
            parametros.Add("Correo",usuario.Correo);
            parametros.Add("Activo",usuario.Activo);
            parametros.Add("Mensaje",dbType: System.Data.DbType.String, 
                direction: System.Data.ParameterDirection.Output, size: 500);
            parametros.Add("Resultado",dbType: System.Data.DbType.Binary, 
                direction: System.Data.ParameterDirection.Output);

            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"sp_EditarUsuarios", parametros,
                                          commandType: System.Data.CommandType.StoredProcedure);

            mensaje = parametros.Get<string>("Mensaje");
            resultado = parametros.Get<bool>("Resultado");
            return resultado;


        }
    }


}
