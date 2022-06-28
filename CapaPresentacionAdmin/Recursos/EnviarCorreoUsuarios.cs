using System.Net;
using System.Net.Mail;

namespace CapaPresentacionAdmin.Recursos
{
    public class EnviarCorreoUsuarios
    {

        public static bool EnviarCorreo(string correo, string asunto, string mensaje) 
        {
            bool resultado = false;
            try 
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("igrismarshalshadow@gmail");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("igrismarshalshadow@gmail", "fdyykwfumlzgmjda"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smtp.Send(mail);
                resultado = true;
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                resultado = false;
            }
            return resultado;
        }
    }
}
