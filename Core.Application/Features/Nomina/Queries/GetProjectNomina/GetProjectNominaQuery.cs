using Core.Application.DTOs.Nomina;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Nomina.Queries.GetProjectNomina
{
    public class GetProjectNominaQuery : IRequest<List<NominaDTO>>
    {
        public int ProjectId { get; set; }
    }
    public class GetProjectNominaQueryHandler : IRequestHandler<GetProjectNominaQuery, List<NominaDTO>>
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IPercepcionesRepository _percepcionesRepository;
        private readonly IDeduccionesRepository _deduccionesRepository;

        public GetProjectNominaQueryHandler(IPagoRepository pagoRepository, IEmpleadoRepository empleadoRepository, IProyectoRepository proyectoRepository, IPercepcionesRepository percepcionesRepository, IDeduccionesRepository deduccionesRepository)
        {
            _pagoRepository = pagoRepository;
            _empleadoRepository = empleadoRepository;
            _proyectoRepository = proyectoRepository;
            _percepcionesRepository = percepcionesRepository;
            _deduccionesRepository = deduccionesRepository;
        }
        
        public async Task<List<NominaDTO>> Handle(GetProjectNominaQuery request, CancellationToken cancellationToken)
        {
            var responsePago = await _pagoRepository.GetAllWithIncludes(new List<string> { "Pago_Percepciones", "Pago_Deducciones", "Empleado", "TipoPago" });
            var responseProyecto = await _proyectoRepository.GetByIdAsync(request.ProjectId);
            List<NominaDTO> response = new List<NominaDTO>();


            foreach (var item in responsePago)
            {
                string deducciones = "";
                decimal sumdeducciones = 0;

                string percepciones = "";
                decimal sumpercepciones = 0;

                foreach (var d in item.Pago_Deducciones) {
                    d.Deducciones = await _deduccionesRepository.GetByIdAsync(d.IdDeducciones);
                    sumdeducciones += d.Deducciones.Monto;
                    deducciones += d.Deducciones.Nombre;
                }
                foreach (var p in item.Pago_Percepciones)
                {
                    p.Percepciones = await _percepcionesRepository.GetByIdAsync(p.IdPercepciones);
                    sumpercepciones += p.Percepciones.Monto;
                    percepciones += p.Percepciones.Nombre;
                }

                response.Add(new NominaDTO()
                {
                    Nombre = item.Empleado.Nombre,
                    Apellido = item.Empleado.Apellido,
                    FechaNacimiento = item.Empleado.FechaNacimiento,
                    Fecha = item.Fecha,

                    Monto = item.Monto,
                    Comision = item.Comision,

                    Deduccion = sumdeducciones,
                    DeduccionDescripcion = "Pago"+ deducciones,

                    Percepcion = sumpercepciones,
                    PercepcionDescripcion = "Pago" + percepciones,

                    NombreProyecto = responseProyecto.Nombre,
                    Neto = item.Monto - sumdeducciones + sumpercepciones
                });
            }


            return response;
        }
    }
}
