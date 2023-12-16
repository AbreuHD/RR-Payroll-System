using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Pago
{
    public class GetPagoByIdDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public int IdTipoPago { get; set; }
        public int IdEmpleado { get; set; }
        public TipoPago TipoPago { get; set; }
        public Empleado Empleado { get; set; }
        public Domain.Entities.Proyecto Proyecto { get; set; }
    }
}
