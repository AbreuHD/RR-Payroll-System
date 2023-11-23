using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.EmpleadoProyecto.Commands.EditEmpleadoProyecto
{
    public class EditEmpleadoProyectoCommand : IRequest<bool>
    {
        public int IdEmpleado { get; set; }
        public int Id { get; set; }
        public int IdPuesto { get; set; }
        public int IdProyecto { get; set; }
    }
    public class EditEmpleadoProyectoCommandHandler : IRequestHandler<EditEmpleadoProyectoCommand, bool>
    {
        private readonly IEmpleadoProyectosRepository _empleadoProyectoRepository;
        private readonly IMapper _mapper;

        public EditEmpleadoProyectoCommandHandler(IEmpleadoProyectosRepository empleadoProyectoRepository, IMapper mapper)
        {
            _empleadoProyectoRepository = empleadoProyectoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EditEmpleadoProyectoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var empleadoProyecto = _mapper.Map<Domain.Entities.EmpleadoProyectos>(request);
                await _empleadoProyectoRepository.UpdateAsync(empleadoProyecto, request.Id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
