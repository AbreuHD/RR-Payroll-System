using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Nomina
{
    public class NominaDTO
    {
        //empleado table
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        //pago table
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        //deducciones table
        public decimal Deduccion { get; set; }
        public string DeduccionDescripcion { get; set; }
        //percepciones table
        public decimal Percepcion { get; set; }
        public string PercepcionDescripcion { get; set; }
        //proyecto table
        public string NombreProyecto { get; set; }
        //neto pay table
        public decimal Neto { get; set; }
    }
}
