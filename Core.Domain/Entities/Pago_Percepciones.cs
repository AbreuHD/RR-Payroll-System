using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Pago_Percepciones : AuditableBase
    {
        public int IdPago { get; set; }
        public int IdPercepciones { get; set; }

        public Pago Pago { get; set; }
        public Percepciones Percepciones { get; set;}
    }
}
