using AutoMapper;
using Core.Application.DTOs.Pago;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Pagos.Queries.GetPagoByUserID
{
    public class GetPagoByUserIDQuery : IRequest<List<GetPagoByIdDTO>>
    {
        public int IdEmpleado { get; set; }
        public int IdProyecto { get; set; }
    }
    public class GetPagoByUserIDQueryHandler : IRequestHandler<GetPagoByUserIDQuery, List<GetPagoByIdDTO>>
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IMapper _mapper;
        private readonly IProyectoRepository _proyectoRepository;

        public GetPagoByUserIDQueryHandler(IPagoRepository pagoRepository, IMapper mapper, IProyectoRepository proyectoRepository)
        {
            _pagoRepository = pagoRepository;
            _mapper = mapper;
            _proyectoRepository = proyectoRepository;
        }

        public async Task<List<GetPagoByIdDTO>> Handle(GetPagoByUserIDQuery request, CancellationToken cancellationToken)
        {
            var entity = await _pagoRepository.GetAllWithIncludes(new List<string> { "Empleado", "TipoPago" });
            var responsePago = entity.Where(x => x.IdEmpleado == request.IdEmpleado).ToList();

            var response = _mapper.Map<List<GetPagoByIdDTO>>(responsePago);
            foreach (var item in response)
            {
                item.Proyecto = await _proyectoRepository.GetByIdAsync(responsePago.FirstOrDefault().IdProyecto);
            }
            return response;
        }
    }
}
