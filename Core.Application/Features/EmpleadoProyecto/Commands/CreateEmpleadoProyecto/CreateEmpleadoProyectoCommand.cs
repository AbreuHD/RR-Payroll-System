using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.EmpleadoProyecto.Commands.CreateEmpleadoProyecto
{
    public class CreateEmpleadoProyectoCommand : IRequest<Core.Domain.Entities.EmpleadoProyectos>
    {
        public int IdPuesto { get; set; }
        public int IdProyecto { get; set; }
        public int IdEmpleado { get; set; }
        public int Horas { get; set; }
        public double PagoHoras { get; set; }
    }

    public class CreateEmpleadoProyectoCommandHandler : IRequestHandler<CreateEmpleadoProyectoCommand, Domain.Entities.EmpleadoProyectos>
    {
        private readonly IEmpleadoProyectosRepository _empleadoProyectoRepository;
        private readonly IMapper _mapper;

        public CreateEmpleadoProyectoCommandHandler(IEmpleadoProyectosRepository empleadoProyectoRepository, IMapper mapper)
        {
            _empleadoProyectoRepository = empleadoProyectoRepository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.EmpleadoProyectos> Handle(CreateEmpleadoProyectoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var empleadoProyecto = _mapper.Map<Domain.Entities.EmpleadoProyectos>(request);
                var response = await _empleadoProyectoRepository.AddAsync(empleadoProyecto);
                return response;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
