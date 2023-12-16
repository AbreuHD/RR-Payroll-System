using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class ActividadesAsignadas : AuditableBase
    {
        public int IdActividad { get; set; }
        public int IdEmpleadoProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Estado { get; set; }
        //public int IdProyecto { get; set; }

        public Actividades Actividad { get; set; }
        public EmpleadoProyectos EmpleadoProyecto { get; set; }
        //public Estado Estado { get; set; }
        //public Proyecto Proyecto { get; set; }
    }
}
