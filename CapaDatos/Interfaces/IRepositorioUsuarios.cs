using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface IRepositorioUsuarios
    {
        Task<bool> Editar(Usuario usuario);
        Task<int> Guardar(Usuario usuario, string mensaje);
        Task<IEnumerable<Usuario>> Obtener();
    }
}
