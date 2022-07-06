using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface ICapaNegocioMarcas
    {
        bool Editar(Marca marca, out string Mensaje);
        bool Eliminar(int id, out string Mensaje);
        int Guardar(Marca marca, out string Mensaje);
    }
}
