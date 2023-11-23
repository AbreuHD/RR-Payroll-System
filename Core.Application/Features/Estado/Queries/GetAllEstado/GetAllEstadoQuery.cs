using AutoMapper;
using Core.Application.DTOs.Estado;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Estado.Queries.GetAllEstado
{
    public class GetAllEstadoQuery : IRequest<List<GetAllStatusDTO>>
    {
    }

    public class GetAllStatusQueryHandler : IRequestHandler<GetAllEstadoQuery, List<GetAllStatusDTO>>
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMapper _mapper;

        public GetAllStatusQueryHandler(IEstadoRepository estadoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _estadoRepository = estadoRepository;
        }
        public async Task<List<GetAllStatusDTO>> Handle(GetAllEstadoQuery request, CancellationToken cancellationToken)
        {
            var estados = await _estadoRepository.GetAllAsync();
            var response = _mapper.Map<List<GetAllStatusDTO>>(estados);
            return response;

        }
    }
}
