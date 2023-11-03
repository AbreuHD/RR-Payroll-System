using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class DetalleNomina : AuditableBase
    {
        public decimal SueldoBruto { get; set; }
        public decimal ISR { get; set; }
        public decimal AFP { get; set; }
        public decimal SFS { get; set; }
        public decimal SueldoNeto { get; set; }

        public int IdProyecto { get; set; }
        public Proyecto Proyecto { get; set; }
    }
}
