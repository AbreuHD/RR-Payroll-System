using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Actividades
{
    public class GetActivityByIdResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public List<Domain.Entities.Empleado> Empleados { get; set; } 
    }
}
