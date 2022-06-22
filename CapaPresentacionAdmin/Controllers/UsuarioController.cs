using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using CapaDatos.Servicio;
using CapaDatos.Interfaces;
using System.Web;



namespace CapaPresentacionAdmin.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IRepositorioUsuarios repositorioUsuarios;

        private readonly ICapaNegocioUsuarios capaNegocioUsuarios;

        public UsuarioController(IRepositorioUsuarios repositorioUsuarios, 
            IClaveEncriptacion claveEncriptacion, ICapaNegocioUsuarios capaNegocioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
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
        public JsonResult GuardarUsuario( Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdUsuario == 0)
            {
                
                resultado = capaNegocioUsuarios.Guardar(objeto, out mensaje);
                  
            }
            else
            {
                resultado = capaNegocioUsuarios.Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje });

        }
        //[HttpPost]
        //public 

       


    }
}
