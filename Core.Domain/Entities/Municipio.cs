using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Municipio : AuditableBase
    {
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }

        public Provincia Provincia { get; set; }
    }
}
