using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Licencia : AuditableBase
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public int IdEmpleado { get; set; }

        public Empleado Empleado { get; set; }
    }
}
