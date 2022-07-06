using CapaDatos.Interfaces;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IRepositorioMarcas repositorioMarcas;
        private readonly ICapaNegocioMarcas capaNegocioMarcas;

        public MarcaController(IRepositorioMarcas repositorioMarcas, 
            ICapaNegocioMarcas capaNegocioMarcas)
        {
            this.repositorioMarcas = repositorioMarcas;
            this.capaNegocioMarcas = capaNegocioMarcas;
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

        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if(objeto.IdMarca == 0)
            {
                resultado = capaNegocioMarcas.Guardar(objeto, out mensaje);
            }
            else
            {
                resultado = capaNegocioMarcas.Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta;
            string mensaje = string.Empty;

            respuesta = capaNegocioMarcas.Eliminar(id, out mensaje);
            return Json(new { respuesta = respuesta, mensaje = mensaje });
        }
    }
}
