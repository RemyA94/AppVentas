using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using CapaDatos.Servicio;
using CapaDatos.Interfaces;
using Newtonsoft.Json;

namespace CapaPresentacionAdmin.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IRepositorioUsuarios repositorioUsuarios;
        private readonly IClaveEncriptacion claveEncriptacion;

        public UsuarioController(IRepositorioUsuarios repositorioUsuarios, IClaveEncriptacion claveEncriptacion)
        {
            this.repositorioUsuarios = repositorioUsuarios;
            this.claveEncriptacion = claveEncriptacion;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ObteniendoUsuarios()
        {
            var data = await repositorioUsuarios.Obtener();
            return Json(data);
        }

        public IRepositorioUsuarios GetRepositorioUsuarios()
        {
            return repositorioUsuarios;
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario usuario)
        {
            object resultado;
            string mensaje = string.Empty;

            if(usuario.IdUsuario == 0) 
            {
                resultado = repositorioUsuarios.Guardar(usuario, mensaje);
            }
            else
            {
                resultado = repositorioUsuarios.Editar(usuario, mensaje);
            }
            return Json(resultado, mensaje);

        }

        [HttpGet]
        public async Task<int> Guardar(Usuario usuario, string mensaje)
        {
            mensaje = string.Empty;


            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                mensaje = "El apelledio del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                mensaje = "El correo del usuario no puede estar vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                //aqui ira la logica para el correo

                string clave = "test123";
                usuario.Clave = claveEncriptacion.ConvertirSha256(clave);
                return await repositorioUsuarios.Guardar(usuario, mensaje);
            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        public async Task<bool> Editar(Usuario usuario, string mensaje) 
        {
            mensaje = string.Empty;


            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El nombre del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                mensaje = "El apelledio del usuario no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                mensaje = "El correo del usuario no puede estar vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
               
                return await repositorioUsuarios.Editar(usuario, mensaje);
            }
            else
            {
                return false;
            }

        }

        [HttpGet]
        public async Task<bool> Eliminar(int id) 
        {
            return await repositorioUsuarios.Eliminar(id);
        }



    }
}
