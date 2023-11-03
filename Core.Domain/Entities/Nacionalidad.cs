using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Nacionalidad : AuditableBase
    {
        public string Nombre { get; set; }
        public ICollection<Empleado> Empleados { get; set; }
    }
}
