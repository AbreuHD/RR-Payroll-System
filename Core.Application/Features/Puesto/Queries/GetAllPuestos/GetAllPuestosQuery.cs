using AutoMapper;
using Core.Application.DTOs.Puestos;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Puesto.Queries.GetAllPuestos
{
    public class GetAllPuestosQuery : IRequest<List<GetAllPuestosResponseDTO>>
    {
    }
    public class GetAllPuestosQueryHandler : IRequestHandler<GetAllPuestosQuery, List<GetAllPuestosResponseDTO>>
    {
        private readonly IPuestoRepository _puestoRepository;
        private readonly IMapper _mapper;

        public GetAllPuestosQueryHandler(IPuestoRepository puestoRepository, IMapper mapper)
        {
            _puestoRepository = puestoRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPuestosResponseDTO>> Handle(GetAllPuestosQuery request, CancellationToken cancellationToken)
        {
            var puestos = await _puestoRepository.GetAllAsync();
            return _mapper.Map<List<GetAllPuestosResponseDTO>>(puestos);
        }
    }
}
