using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Contrato : AuditableBase
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }

        public ICollection<EmpleadoProyecto> EmpleadoProyectos { get; set; }
    }
}
