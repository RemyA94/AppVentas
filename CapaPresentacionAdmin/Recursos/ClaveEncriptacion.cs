using CapaDatos.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CapaPresentacionAdmin.Recursos
{
    public class ClaveEncriptacion : IClaveEncriptacion
    {

        public string ConvertirSha256(string texto) 
        {
            StringBuilder stringBuilder= new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(texto));

                foreach(byte b in result)
                    stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }


    }
}
