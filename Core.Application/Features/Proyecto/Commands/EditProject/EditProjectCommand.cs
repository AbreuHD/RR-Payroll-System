using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Proyecto.Commands.EditProject
{
    public class EditProjectCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Locacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int IdEstado { get; set; }
        public string Cliente { get; set; }
    }
    public class EditProjectCommandHandler : IRequestHandler<EditProjectCommand, bool>
    {
        public readonly IProyectoRepository _proyectoRepository;
        public readonly IMapper _mapper;

        public EditProjectCommandHandler(IProyectoRepository projectRepository, IMapper mapper)
        {
            _proyectoRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EditProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var proyecto = _mapper.Map<Domain.Entities.Proyecto>(request);
                await _proyectoRepository.UpdateAsync(proyecto, request.Id);
                return true;
            }catch(Exception e)
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
