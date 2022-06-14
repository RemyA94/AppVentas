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
                @"select Nombre, Apellido, 
                Correo, Clave, Activo from Usuario");

        }

    }


}
