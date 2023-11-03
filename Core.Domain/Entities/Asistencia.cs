using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Asistencia : AuditableBase
    {
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdEmpleadoProyecto { get; set; }

        public EmpleadoProyecto EmpleadoProyecto { get; set; }
        public ICollection<Horas> Horas { get; set; }
    }
}
