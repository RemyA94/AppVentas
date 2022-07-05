using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface ICapaNegocioCategorias
    {
        bool Editar(Categoria categoria, out string Mensaje);
        bool Eliminar(int id, out string Mensaje);
        int Guardar(Categoria categoria, out string Mensaje);
    }
}
