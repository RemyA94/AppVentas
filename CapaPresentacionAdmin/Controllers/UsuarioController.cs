using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using CapaDatos.Servicio;

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
        public async Task<IActionResult> ListarUsuarios2()
        {
            var usurios = await repositorioUsuarios.Obtener();
            return Json(usurios);
        }
    }
}
