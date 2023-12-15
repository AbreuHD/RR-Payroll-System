using AutoMapper;
using Core.Application.DTOs.Percepciones;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Percepciones.Queries.GetAllPercepcion
{
    public class GetAllPercepcionQuery : IRequest<List<GetAllPercepcionResponseDTO>>
    {
    }
    public class GetAllPercepcionQueryHandler : IRequestHandler<GetAllPercepcionQuery, List<GetAllPercepcionResponseDTO>>
    {
        private readonly IPercepcionesRepository _percepcionRepository;
        private readonly IMapper _mapper;

        public GetAllPercepcionQueryHandler(IPercepcionesRepository percepcionRepository, IMapper mapper)
        {
            _percepcionRepository = percepcionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPercepcionResponseDTO>> Handle(GetAllPercepcionQuery request, CancellationToken cancellationToken)
        {
            var responsePercepcion = await _percepcionRepository.GetAllAsync();
            var response = _mapper.Map<List<GetAllPercepcionResponseDTO>>(responsePercepcion);
            return response;
        }
    }
}
