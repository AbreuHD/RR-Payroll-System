using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class EmpleadoProyectos : AuditableBase
    {
        //public bool EsEncargado { get; set; }
        public int IdPuesto { get; set; }
        //public int IdCOntrato { get; set; }
        public int IdProyecto { get; set; }
        public int IdEmpleado { get; set; }


        public Puesto Puesto { get; set; }
        //public Contrato Contrato { get; set; }
        public Proyecto Proyecto { get; set; }
        public Empleado Empleado { get; set; }
        public Asistencia Asistencia { get; set; }
        public ICollection<ActividadesAsignadas> ActividadesAsignadas { get; set; }
    }
}
