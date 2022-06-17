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
        private readonly ICapaNegocioUsuarios capaNegocioUsuarios;

        public UsuarioController(IRepositorioUsuarios repositorioUsuarios, 
            IClaveEncriptacion claveEncriptacion, ICapaNegocioUsuarios capaNegocioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
            this.claveEncriptacion = claveEncriptacion;
            this.capaNegocioUsuarios = capaNegocioUsuarios;
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

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario usuario)
        {
            object resultado;
            string mensaje = string.Empty;

            if (usuario.IdUsuario == 0)
            {
                resultado = capaNegocioUsuarios.Guardar(usuario, out mensaje);
                  
            }
            else
            {
                resultado = capaNegocioUsuarios.Editar(usuario, out mensaje);
            }
            return Json(resultado, mensaje);

        }


       



    }
}
