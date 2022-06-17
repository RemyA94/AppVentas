using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface ICapaNegocioUsuarios
    {
        bool Editar(Usuario usuario, out string Mensaje);
        bool Eliminar(int id, out string Mensaje);
        int Guardar(Usuario usuario, out string Mensaje);
    }
}
