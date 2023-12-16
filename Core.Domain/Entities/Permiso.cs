using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Permiso : AuditableBase
    {
        public DateTime FechaInicio { get; set; }
        public String Motivo { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Pagado { get; set; }
        public int IdAsistencia { get; set; }

        public Asistencia Asistencia { get; set; }
    }
}
