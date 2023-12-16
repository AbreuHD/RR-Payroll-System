using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Empleado : AuditableBase
    {
        public string UserID { get; set; } //FK
        //public string Documento { get; set; }
        //public int Codigo { get; set; }
        //public string NumCuenta { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Sexo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        //public string tipoPago { get; set; }
        
        public int IdNacionalidad { get; set; }
        public int IdProvincia { get; set; }
        public int IdEstado { get; set; }

        public Nacionalidad Nacionalidad { get; set; }
        public Provincia Provincia { get; set; }
        public Estado Estado { get; set; }

        public ICollection<Licencia> Licencias { get; set; }
        public ICollection<Pago> Pagos { get; set; }
        public ICollection<EmpleadoProyectos> EmpleadoProyectos { get; set; }
    }
}
