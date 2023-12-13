using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Deducciones : AuditableBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public bool IsDefault { get; set; }

        //public decimal Porcentaje { get; set; }

        public ICollection<Pago> Pagos { get; set; }
    }
}
