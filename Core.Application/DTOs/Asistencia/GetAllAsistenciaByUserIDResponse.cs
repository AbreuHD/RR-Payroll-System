using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Asistencia
{
    public class GetAllAsistenciaByUserIDResponse
    {
        public int Id { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdEmpleadoProyecto { get; set; }

        public List<AsistenciaResponseDTO> Asistencia { get; set; }

        public List<Horas> Horas { get; set; }
        public List<Permiso> Permiso { get; set; }
    }
}
