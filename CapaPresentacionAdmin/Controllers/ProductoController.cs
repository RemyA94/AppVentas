using CapaDatos.Interfaces;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Configuration;
using System.IO;

namespace CapaPresentacionAdmin.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepositorioProductos repositorioProductos;
        private readonly ICapaNegocioProductos capaNegocioProductos;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConvertirImagenBase64 convertirImagenBase64;


        public ProductoController(IRepositorioProductos repositorioProductos,
            ICapaNegocioProductos capaNegocioProductos, IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, IConvertirImagenBase64 convertirImagenBase64)
        {
            this.repositorioProductos = repositorioProductos;
            this.capaNegocioProductos = capaNegocioProductos;
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
            this.convertirImagenBase64 = convertirImagenBase64;
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

        [HttpPost]
        public JsonResult GuardarProducto(string objeto, IFormFile archivoImagen )
        {
            string mensaje = string.Empty;
            bool operacionExitosa = true;
            bool imgGuardada = true;

            Producto producto = new Producto();
            producto= JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;

            if(decimal.TryParse(producto.PrecioTexto,NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out precio)) 
            {
                producto.Precio = precio;
            
            }
            else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe de ser ##.##" });
            }

            if (producto.IdProducto == 0)
            {
                int idProductoGenerado = capaNegocioProductos.Guardar(producto, out mensaje);
                if(idProductoGenerado != 0)
                {
                    producto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacionExitosa = false;
                }
            }
            else
            {
                operacionExitosa = capaNegocioProductos.Editar(producto, out mensaje);
            }

            if (operacionExitosa)
            {
                if(archivoImagen != null)
                {
                    string rutaGuardar = configuration["ServidorFotos:value"];
                    string extension = Path.GetExtension(archivoImagen.FileName); //obtenemos la extension de la imagen
                    string nombreImg = string.Concat(producto.IdProducto.ToString(), extension); //gaurdamos el nombre de la imagen con el id de la imagen y su su extencion

                    var path = Path.Combine(rutaGuardar, nombreImg);

                    try
                    {
                        using (var stream = new FileStream(path, FileMode.Create)) 
                        {
                            archivoImagen.CopyTo(stream);
                        }

                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        imgGuardada = false;
                    }
                    if (imgGuardada)
                    {
                        producto.RutaImagen = rutaGuardar;
                        producto.NombreImagen = nombreImg;
                        bool respuesta = capaNegocioProductos.GuardarDatosImagen(producto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto, pero hubo problemas con la imagen";
                    }
                }
            }
            return Json(new { operacionExitosa = operacionExitosa, idGenerado= producto.IdProducto, mensaje = mensaje });
        }


        [HttpPost] //Este metodo nos devuelve la configuarcion en base64 de una imagen, en ves de mostrar el nombre mostramos la conversion
        public JsonResult ImagenProducto( int id)
        {
            bool conversion;
            Producto producto = repositorioProductos.Obtener().Where(p => p.IdProducto == id).FirstOrDefault();

            string textBase64 = convertirImagenBase64.ConvertirBase64(Path.Combine(producto.RutaImagen, producto.NombreImagen), out conversion );
            
            return Json( new {
                conversion = conversion,
                textBase64 = textBase64,
                extension = Path.GetExtension(producto.NombreImagen)
            });
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta;
            string mensaje = string.Empty;

            respuesta = capaNegocioProductos.Eliminar(id, out mensaje);
            return Json(new { respuesta = respuesta, mensaje = mensaje });
        }
    }
}
