using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Interfaces
{
    public interface IConvertirImagenBase64
    {
        string ConvertirBase64(string ruta, out bool conversion);
    }
}
