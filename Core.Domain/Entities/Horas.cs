using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Horas : AuditableBase
    {
        public string HorasTrabajadas { get; set; }
        public string HorasNormales { get; set; }
        public string HorasExtras { get; set; }
        public int IdAsistencia { get; set; }

        public Asistencia Asistencia { get; set; }
    }
}
