using CapaDatos.Interfaces;

namespace CapaPresentacionAdmin.Recursos
{
    public class ConvertirImagenBase64 : IConvertirImagenBase64
    {
        public string ConvertirBase64(string ruta, out bool conversion)
        {
            string textBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textBase64 = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }
            return textBase64;
        }
        
    }
}
