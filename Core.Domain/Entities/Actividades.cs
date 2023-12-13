using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Actividades : AuditableBase
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        
        //public int IdActividadAsignada { get; set; }
        //public ActividadesAsignadas ActividadesAsignadas { get; set; }
        public ICollection<ActividadesAsignadas> ActividadesAsignadas { get; set; }
    }
}
