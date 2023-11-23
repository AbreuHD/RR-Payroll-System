using AutoMapper;
using Core.Application.DTOs.Proyecto;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Proyecto.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<ProyectoDTO>
    {
        public int Id { get; set; }
    }

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProyectoDTO>
    {
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IEmpleadoProyectosRepository _empleadoProyectoRepository;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IProyectoRepository proyectoRepository, IMapper mapper, IEmpleadoProyectosRepository empleadoProyectoRepository)
        {
            _empleadoProyectoRepository = empleadoProyectoRepository;
            _mapper = mapper;
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ProyectoDTO> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(request.Id);
            var response = _mapper.Map<ProyectoDTO>(proyecto);
            var infodata = await _empleadoProyectoRepository.GetAllAsync(request.Id);
            response.EmpleadoProyectos = infodata.Where(x => x.IdProyecto == request.Id).ToList();
            return response;
        }
    }
}
