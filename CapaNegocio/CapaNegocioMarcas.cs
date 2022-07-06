using CapaDatos.Interfaces;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CapaNegocioMarcas
    {
        private readonly IRepositorioMarcas repositorioMarcas;

        public CapaNegocioMarcas(IRepositorioMarcas repositorioMarcas)
        {
            this.repositorioMarcas = repositorioMarcas;
        }
        public int Guardar(Marca marca, out string Mensaje)
        {
            Mensaje = String.Empty;
            if(string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                Mensaje = "La descripción de la marca no puede estar vacía";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return repositorioMarcas.Guardar(marca, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Marca marca, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(marca.Descripcion)|| string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                Mensaje = "La descripción de la marca no puede estar vacía";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return repositorioMarcas.Editar(marca, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return repositorioMarcas.Eliminar(id, out Mensaje);
        }
    }
}
