using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.ActividadesAsignadas
{
    public class GetActividadesAsignadasByUserResponseDTO
    {
        public int Id { get; set; }
        public int IdActividad { get; set; }
        public int IdEmpleadoProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdEstado { get; set; }

        public Domain.Entities.Actividades Actividad { get; set; }
        public EmpleadoProyectos EmpleadoProyecto { get; set; }
        public Domain.Entities.Estado Estado { get; set; }
    }
}
