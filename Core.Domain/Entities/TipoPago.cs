using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class TipoPago : AuditableBase
    {
        public string Cuenta { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdTipoBanco { get; set; }
        public string IdUsuario { get; set; }

        public TipoBanco TipoBanco { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public ICollection<Pago> Pagos { get; set; }
    }
}
