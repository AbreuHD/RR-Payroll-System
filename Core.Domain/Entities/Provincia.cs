using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Provincia : AuditableBase
    {
        public string Nombre { get; set; }

        public ICollection<Municipio> Municipios { get; set; }
        public Empleado Empleado { get; set; }
    }
}
