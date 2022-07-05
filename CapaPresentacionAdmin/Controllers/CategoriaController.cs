using CapaDatos.Interfaces;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IRepositorioCategorias repositorioCategorias;
        private readonly ICapaNegocioCategorias capaNegocioCategorias;

        public CategoriaController(IRepositorioCategorias repositorioCategorias,
            ICapaNegocioCategorias capaNegocioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
            this.capaNegocioCategorias = capaNegocioCategorias;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerCategoria()
        {
            var data = await repositorioCategorias.Obtener();
            return Json(data);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = capaNegocioCategorias.Guardar(objeto, out mensaje);
            }
            else
            {
                resultado = capaNegocioCategorias.Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta;
            string mensaje = string.Empty;

            respuesta = capaNegocioCategorias.Eliminar(id, out mensaje);
            return Json(new {respuesta = respuesta, mensaje = mensaje });  
        }

    }
}
