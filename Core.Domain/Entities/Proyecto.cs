using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Proyecto : AuditableBase
    {
        public string Nombre { get; set; }  
        public string Cliente { get; set; }
        public string Locacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int IdEstado { get; set; }

        public Estado Estado { get; set; }
        public ICollection<DetalleNomina> DetalleNominas { get; set; }
        public ICollection<EmpleadoProyecto> EmpleadoProyectos { get; set; }
     }
}
