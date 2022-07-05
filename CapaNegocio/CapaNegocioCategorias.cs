using CapaDatos.Interfaces;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CapaNegocioCategorias: ICapaNegocioCategorias
    {
        private readonly IRepositorioCategorias repositorioCategorias;

        public CapaNegocioCategorias(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }


        public int Guardar(Categoria categoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                Mensaje = "La descripción de la categoría no puede ser nula";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return repositorioCategorias.Guardar(categoria, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Categoria categoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(String.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                Mensaje = "La descripción de la categoría no puede ser nula";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return repositorioCategorias.Editar(categoria, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return repositorioCategorias.Eliminar(id, out Mensaje);
        }
    }
}
