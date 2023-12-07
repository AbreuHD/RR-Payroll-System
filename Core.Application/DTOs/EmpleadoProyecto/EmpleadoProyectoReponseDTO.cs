using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.EmpleadoProyecto
{
    public class EmpleadoProyectoReponseDTO
    {
        public int Id { get; set; }
        public int IdPuesto { get; set; }
        public int IdProyecto { get; set; }
        public int IdEmpleado { get; set; }


        public Puesto Puesto { get; set; }
        public Domain.Entities.Proyecto Proyecto { get; set; }
        public Empleado Empleado { get; set; }
        public Asistencia Asistencia { get; set; }
    }
}
