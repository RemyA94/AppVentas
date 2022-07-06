using CapaDatos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IRepositorioMarcas repositorioMarcas;

        public MarcaController(IRepositorioMarcas repositorioMarcas)
        {
            this.repositorioMarcas = repositorioMarcas;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerMarcas()
        {
            var data = await repositorioMarcas.Obtener();
            return Json(data);
        }
    }
}
