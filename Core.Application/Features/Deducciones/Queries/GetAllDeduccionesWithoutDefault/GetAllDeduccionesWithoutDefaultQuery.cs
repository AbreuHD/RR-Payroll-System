using AutoMapper;
using Core.Application.DTOs.Deducciones;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Deducciones.Queries.GetAllDeduccionesWithoutDefault
{
    public class GetAllDeduccionesWithoutDefaultQuery : IRequest<List<GetAllDeduccionesResponseDTO>>
    {
    }
    public class GetAllDeduccionesWithoutDefaultQueryHandler : IRequestHandler<GetAllDeduccionesWithoutDefaultQuery, List<GetAllDeduccionesResponseDTO>>
    {
        private readonly IDeduccionesRepository _deduccionesRepository;
        private readonly IMapper _mapper;
        public GetAllDeduccionesWithoutDefaultQueryHandler(IDeduccionesRepository deduccionesRepository, IMapper mapper)
        {
            _deduccionesRepository = deduccionesRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllDeduccionesResponseDTO>> Handle(GetAllDeduccionesWithoutDefaultQuery request, CancellationToken cancellationToken)
        {
            var responseDeduccion = await _deduccionesRepository.GetAllAsync();
            var responseDeduccionDefault = responseDeduccion.Where(x => x.IsDefault == false).ToList();
            var response = _mapper.Map<List<GetAllDeduccionesResponseDTO>>(responseDeduccionDefault);
            return response;
        }
    }
}
