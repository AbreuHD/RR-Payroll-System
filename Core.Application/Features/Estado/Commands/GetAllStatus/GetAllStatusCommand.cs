using AutoMapper;
using Core.Application.DTOs.Estado;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Estado.Commands.GetAllStatus
{
    public class GetAllStatusCommand : IRequest<List<GetAllStatusDTO>>
    {
    }

    public class GetAllStatusCommandHandler : IRequestHandler<GetAllStatusCommand, List<GetAllStatusDTO>>
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMapper _mapper;

        public GetAllStatusCommandHandler(IEstadoRepository estadoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _estadoRepository = estadoRepository;
        }
        public async Task<List<GetAllStatusDTO>> Handle(GetAllStatusCommand request, CancellationToken cancellationToken)
        {
            var estados = await _estadoRepository.GetAllAsync();
            var response = _mapper.Map<List<GetAllStatusDTO>>(estados);
            return response;

        }
    }
}
