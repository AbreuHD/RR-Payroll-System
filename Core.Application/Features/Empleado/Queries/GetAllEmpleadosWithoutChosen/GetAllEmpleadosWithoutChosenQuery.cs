using AutoMapper;
using Core.Application.DTOs.Empleados;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Empleado.Queries.GetAllEmpleadosWithoutChosen
{
    public class GetAllEmpleadosWithoutChosenQuery : IRequest<List<GetAllEmpleadosResponseDTO>>
    {
        public List<Domain.Entities.Empleado> Empleados { get; set; }
        public int IdProyecto { get; set; }
    }
    public class GetAllEmpleadosWithoutChosenQueryHandler : IRequestHandler<GetAllEmpleadosWithoutChosenQuery, List<GetAllEmpleadosResponseDTO>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;
        private readonly IEmpleadoProyectosRepository _empleadoProyectosRepository;

        public GetAllEmpleadosWithoutChosenQueryHandler(IEmpleadoRepository empleadoRepository, IMapper mapper, IEmpleadoProyectosRepository empleadoProyectosRepository)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
            _empleadoProyectosRepository = empleadoProyectosRepository;
        }

        public async Task<List<GetAllEmpleadosResponseDTO>> Handle(GetAllEmpleadosWithoutChosenQuery request, CancellationToken cancellationToken)
        {
            var requestEmpleados = await _empleadoRepository.GetAllAsync();
            var requestEmpleadoProyecto = await _empleadoProyectosRepository.GetAllAsync();

            var employeesToRemove = new List<Domain.Entities.Empleado>();

            foreach (var i in requestEmpleados)
            {
                foreach (var j in request.Empleados)
                {
                    if (i.Id == j.Id)
                    {
                        employeesToRemove.Add(i);
                    }
                }
            }

            foreach (var employeeToRemove in employeesToRemove)
            {
                requestEmpleados.Remove(employeeToRemove);
            }

            var responseDTO = _mapper.Map<List<GetAllEmpleadosResponseDTO>>(requestEmpleados);
            List<GetAllEmpleadosResponseDTO> response = new();

            foreach (var i in requestEmpleadoProyecto)
            {
                foreach (var x in responseDTO)
                {
                    if (i.IdEmpleado == x.Id && i.IdProyecto == request.IdProyecto)
                    {
                        x.EmpleadoProyectos = i;
                        response.Add(x);
                    }
                }
            }

            return response;
        }

    }
}
