using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Asistencia
{
    public class AsistenciaResponseDTO
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Final { get; set; }
        public bool IsPagado { get; set; }
    }
}
