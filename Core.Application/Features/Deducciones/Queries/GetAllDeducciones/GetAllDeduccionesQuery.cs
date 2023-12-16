using AutoMapper;
using Core.Application.DTOs.Deducciones;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Deducciones.Queries.GetAllDeducciones
{
    public class GetAllDeduccionesQuery : IRequest<List<GetAllDeduccionesResponseDTO>>
    {
    }
    public class GetAllDeduccionesQueryHandler : IRequestHandler<GetAllDeduccionesQuery, List<GetAllDeduccionesResponseDTO>>
    {
        private readonly IDeduccionesRepository _deduccionesRepository;
        private readonly IMapper _mapper;
        public GetAllDeduccionesQueryHandler(IDeduccionesRepository deduccionesRepository, IMapper mapper)
        {
            _deduccionesRepository = deduccionesRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllDeduccionesResponseDTO>> Handle(GetAllDeduccionesQuery request, CancellationToken cancellationToken)
        {
            var responseDeduccion = await _deduccionesRepository.GetAllAsync();
            var response = _mapper.Map<List<GetAllDeduccionesResponseDTO>>(responseDeduccion);
            return response;
        }
    }
}
