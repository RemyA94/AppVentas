using CapaDatos.Interfaces;

namespace CapaPresentacionAdmin.Recursos
{
    public class GenerarClaveUsuario: IGenerarClaveUsuario
    {
        public string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString( "N").Substring(1,6);
            return clave;
        }
    }
}
