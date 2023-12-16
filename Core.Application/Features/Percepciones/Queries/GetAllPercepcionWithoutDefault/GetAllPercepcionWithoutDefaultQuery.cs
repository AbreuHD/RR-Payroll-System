using AutoMapper;
using Core.Application.DTOs.Percepciones;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Percepciones.Queries.GetAllPercepcionWithoutDefault
{
    public class GetAllPercepcionWithoutDefaultQuery : IRequest<List<GetAllPercepcionResponseDTO>>
    {
    }
    public class GetAllPercepcionWithoutDefaultQueryHandler : IRequestHandler<GetAllPercepcionWithoutDefaultQuery, List<GetAllPercepcionResponseDTO>>
    {
        private readonly IPercepcionesRepository _percepcionRepository;
        private readonly IMapper _mapper;

        public GetAllPercepcionWithoutDefaultQueryHandler(IPercepcionesRepository percepcionRepository, IMapper mapper)
        {
            _percepcionRepository = percepcionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPercepcionResponseDTO>> Handle(GetAllPercepcionWithoutDefaultQuery request, CancellationToken cancellationToken)
        {
            var responsePercepcion = await _percepcionRepository.GetAllAsync();
            var response = _mapper.Map<List<GetAllPercepcionResponseDTO>>(responsePercepcion);
            return response;
        }
    }
}
