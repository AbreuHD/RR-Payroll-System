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
    public class CreateEmpleadoProyectoCommand : IRequest<bool>
    {
        public bool EsEncargado { get; set; }
        public int IdPuesto { get; set; }
        public int IdCOntrato { get; set; }
        public int IdProyecto { get; set; }
        public int IdEmpleado { get; set; }
    }

    public class CreateEmpleadoProyectoCommandHandler : IRequestHandler<CreateEmpleadoProyectoCommand, bool>
    {
        private readonly IEmpleadoProyectoRepository _empleadoProyectoRepository;
        private readonly IMapper _mapper;

        public CreateEmpleadoProyectoCommandHandler(IEmpleadoProyectoRepository empleadoProyectoRepository, IMapper mapper)
        {
            _empleadoProyectoRepository = empleadoProyectoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateEmpleadoProyectoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var empleadoProyecto = _mapper.Map<Domain.Entities.EmpleadoProyecto>(request);
                await _empleadoProyectoRepository.AddAsync(empleadoProyecto);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
