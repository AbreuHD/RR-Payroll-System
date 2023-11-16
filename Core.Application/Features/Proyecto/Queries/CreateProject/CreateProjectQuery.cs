using AutoMapper;
using Core.Application.DTOs.Proyecto;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Proyecto.Queries.CreateProject
{
    public class CreateProjectQuery : IRequest<bool>
    {
        public string Nombre { get; set; }
        public string Cliente { get; set; }
        public string Locacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int IdEstado { get; set; }
    }

    public class CreateProjectQueryHandler : IRequestHandler<CreateProjectQuery, bool>
    {
        public readonly IProyectoRepository _proyectoRepository;
        public readonly IMapper _mapper;

        public CreateProjectQueryHandler(IProyectoRepository proyectoRepository, IMapper mapper)
        {
            _proyectoRepository = proyectoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateProjectQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var proyecto = _mapper.Map<Domain.Entities.Proyecto>(request);
                await _proyectoRepository.AddAsync(proyecto);
                return true;
            }catch(Exception e)
            {
                return false;
            }


        }
    }
}
