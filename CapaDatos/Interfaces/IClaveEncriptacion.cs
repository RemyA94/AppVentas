using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface IClaveEncriptacion
    {
        string ConvertirSha256(string texto);
    }
}
