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

        public UsuarioController(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
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
        public async Task<IActionResult> CrearUsuarios()
        {
            
        } 


        
    }
}
