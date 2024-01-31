using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Register
{
    public class RegisterRequestDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int Genero { get; set; }
        public string Direccion { get; set; }
        public int Estado { get; set; }
        public int TipoCuenta { get; set; }
        public string Cuenta { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public DateTime Nacimiento { get; set; }
        public int Provincia { get; set; }
        public int Nacionalidad { get; set; }
        public int Banco { get; set; }
        public string Correo { get; set; }

        public int IdTipoPago { get; set; }
        public int IdEmpleado { get; set; }
    }
}
