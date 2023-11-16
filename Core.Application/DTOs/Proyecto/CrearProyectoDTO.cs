using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Proyecto
{
    public class CrearProyectoDTO
    {
        public string Nombre { get; set; }
        public string Cliente { get; set; }
        public string Locacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int IdEstado { get; set; }
    }
}
