using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }


        public  string PrecioTexto { get; set; } //para poder recibir el valor desde la web
        public string Base64 { get; set; } //formato en cual vamos a mostrar las  imagenes
        public string extension { get; set; } //Extension de la imgaen que vamos a subir
    }
}
