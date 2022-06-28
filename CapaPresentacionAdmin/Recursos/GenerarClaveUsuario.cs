namespace CapaPresentacionAdmin.Recursos
{
    public class GenerarClaveUsuario
    {
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString( "N").Substring(1,6);
            return clave;
        }
    }
}
