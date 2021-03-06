using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface ICapaNegocioProductos
    {
        bool Editar(Producto producto, out string Mensaje);
        bool Eliminar(int id, out string Mensaje);
        int Guardar(Producto producto, out string Mensaje);
        bool GuardarDatosImagen(Producto producto, out string Mensaje);
    }
}
