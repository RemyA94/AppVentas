using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface IEnviarCorreoUsuarios
    {
        bool EnviarCorreo(string correo, string asunto, string mensaje);
    }
}
