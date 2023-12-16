using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Pago_Deducciones : AuditableBase
    {
        public int IdPago { get; set; }
        public int IdDeducciones { get; set; }

        public Pago Pago { get; set; }
        public Deducciones Deducciones { get; set; }
    }
}
