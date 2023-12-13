using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Pago : AuditableBase
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public string Emisor { get; set; }
        
        public int IdTipoPago { get; set; }
        public int IdEmpleado { get; set; }
        public int IdPercepciones { get; set; }
        public int IdDeducciones { get; set; }

        public TipoPago TipoPago { get; set; }
        public Empleado Empleado { get; set; }
        public Percepciones Percepciones { get; set; }
        public Deducciones Deducciones { get; set; }
    }
}
