using CapaDatos.Interfaces;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CapaNegocioProductos : ICapaNegocioProductos
    {
        private readonly IRepositorioProductos repositorioProductos;

        public CapaNegocioProductos(IRepositorioProductos repositorioProductos)
        {
            this.repositorioProductos = repositorioProductos;
        }


        public int Guardar(Producto producto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser nulo";
            }
            else if (string.IsNullOrEmpty(producto.Descripcion) || string.IsNullOrWhiteSpace(producto.Descripcion))
            {
                Mensaje = "La descripción del producto no puede ser nulo";
            }
            else if (producto.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (producto.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría";
            }
            else if (producto.Precio == 0)
            {
                Mensaje = "Debe ingresar el precion del producto";
            }
            else if (producto.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return repositorioProductos.Guardar(producto, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Producto producto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser nulo";
            }
            else if (string.IsNullOrEmpty(producto.Descripcion) || string.IsNullOrWhiteSpace(producto.Descripcion))
            {
                Mensaje = "La descripción del producto no puede ser nulo";
            }
            else if (producto.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (producto.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría";
            }
            else if (producto.Precio == 0)
            {
                Mensaje = "Debe ingresar el precion del producto";
            }
            else if (producto.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return repositorioProductos.Editar(producto, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Producto producto, out string Mensaje)
        {
            return repositorioProductos.GuardarDatosImagen(producto, out Mensaje);
        
        }

            public bool Eliminar(int id, out string Mensaje)
        {
            return repositorioProductos.Eliminar(id, out Mensaje);
        }

    }
}
