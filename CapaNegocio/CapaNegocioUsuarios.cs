using CapaDatos.Interfaces;
using CapaDatos.Servicio;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace CapaNegocio
{

    public class CapaNegocioUsuarios : ICapaNegocioUsuarios
    {
        private readonly IClaveEncriptacion claveEncriptacion;
        private readonly IRepositorioUsuarios repositorioUsuarios;
        private readonly IGenerarClaveUsuario generarClaveUsuario;
        private readonly IEnviarCorreoUsuarios enviarCorreoUsuarios;

        public CapaNegocioUsuarios(IClaveEncriptacion claveEncriptacion,
            IRepositorioUsuarios repositorioUsuarios, 
            IGenerarClaveUsuario generarClaveUsuario, 
            IEnviarCorreoUsuarios enviarCorreoUsuarios)
        {
            this.claveEncriptacion = claveEncriptacion;
            this.repositorioUsuarios = repositorioUsuarios;
            this.generarClaveUsuario = generarClaveUsuario;
            this.enviarCorreoUsuarios = enviarCorreoUsuarios;
        }
        
        public int Guardar(Usuario usuario, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                Mensaje = "El nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                Mensaje = "El apellido del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                Mensaje = "El correo del usuario no puede estar vacio";

            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                //Enviar un correo con la clave de acceso para los nuevos usuarios
                string clave = generarClaveUsuario.GenerarClave();

                string asunto = "Creacion de cuenta";
                string mensajeCorreo = "<h3>Su cuenta fué creada correctamente</h3></br><p>Su contraseña para acceder es: ¡clave!</p>";
                mensajeCorreo = mensajeCorreo.Replace("¡clave!", clave);

                bool respuesta = enviarCorreoUsuarios.EnviarCorreo(usuario.Correo, asunto, mensajeCorreo);

                if (respuesta)
                {
                    usuario.Clave = claveEncriptacion.ConvertirSha256(clave);
                    return repositorioUsuarios.Guardar(usuario, out Mensaje);
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }


        public bool Editar(Usuario usuario, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                Mensaje = "El nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                Mensaje = "El apelledio del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                Mensaje = "El correo del usuario no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return repositorioUsuarios.Editar(usuario, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return repositorioUsuarios.Eliminar(id, out Mensaje);
        }

    }
}
