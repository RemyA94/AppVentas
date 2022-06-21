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

        public CapaNegocioUsuarios(IClaveEncriptacion claveEncriptacion,
            IRepositorioUsuarios repositorioUsuarios)
        {
            this.claveEncriptacion = claveEncriptacion;
            this.repositorioUsuarios = repositorioUsuarios;
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
                //aqui ira la logica para el correo

                string clave = "test123";
                usuario.Clave = claveEncriptacion.ConvertirSha256(clave);
                return repositorioUsuarios.Guardar(usuario, out Mensaje);
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
