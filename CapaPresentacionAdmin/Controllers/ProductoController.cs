using CapaDatos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepositorioProductos repositorioProductos;
        private readonly ICapaNegocioProductos capaNegocioProductos;

        public ProductoController(IRepositorioProductos repositorioProductos,
            ICapaNegocioProductos capaNegocioProductos)
        {
            this.repositorioProductos = repositorioProductos;
            this.capaNegocioProductos = capaNegocioProductos;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerProductos()
        {
            var data =  repositorioProductos.Obtener();
            return Json(data);
        }
    }
}
