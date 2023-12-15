using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Percepciones
{
    public class GetAllPercepcionResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public bool IsDefault { get; set; }
    }
}
