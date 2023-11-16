using AutoMapper;
using Core.Application.DTOs.Proyecto;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Proyecto.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<List<ProyectoDTO>>
    {
    }

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProyectoDTO>>
    {
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IProyectoRepository proyectoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _proyectoRepository = proyectoRepository;
        }

        public async Task<List<ProyectoDTO>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var proyectos = await _proyectoRepository.GetAllAsync();
            var response = _mapper.Map<List<ProyectoDTO>>(proyectos);
            return response;
        }
    }
}
