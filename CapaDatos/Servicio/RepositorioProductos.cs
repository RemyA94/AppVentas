using CapaDatos.Interfaces;
using CapaEntidad;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<IEnumerable<Producto>> Obtener() 
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Producto>(
                                    @"Select p.IdProducto, p.Nombre, p.Descripcion,
                                    m.IdMarca, m.Descripcion[DesMarca],
                                    c.IdCategoria, c.Descripcion[DesCategiria],
                                    p.Precio, p.Stock, p.NombreImagen, p.RutaImagen, p.Activo
                                    from Producto p
                                    inner join Marca m on m.IdMarca = p.IdMarca
                                    inner join Categoria c on c.IdCategoria = p.IdCategoria");
        }

        public int 
    }
}
