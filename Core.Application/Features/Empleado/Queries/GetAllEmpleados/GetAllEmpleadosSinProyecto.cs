using AutoMapper;
using Core.Application.DTOs.Empleados;
using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Empleado.Queries.GetAllEmpleados
{
    public class GetAllEmpleadosSinProyecto : IRequest<List<GetAllEmpleadosResponseDTO>>
    {
        public List<EmpleadoProyectos> EmpleadoProyectos { get; set; }
    }

    public class GetAllEmpleadosSinProyectoHandler : IRequestHandler<GetAllEmpleadosSinProyecto, List<GetAllEmpleadosResponseDTO>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public GetAllEmpleadosSinProyectoHandler(IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllEmpleadosResponseDTO>> Handle(GetAllEmpleadosSinProyecto request, CancellationToken cancellationToken)
        {
            var empleados = await _empleadoRepository.GetAllAsync();
            List<Domain.Entities.Empleado> empleadosToRemove = new();

            if(request.EmpleadoProyectos != null)
            {
                foreach (var i in request.EmpleadoProyectos)
                {
                    foreach (var j in empleados)
                    {
                        if (i.IdEmpleado == j.Id)
                        {
                            empleadosToRemove.Add(j);
                        }
                    }
                }
                foreach (var empleadoToRemove in empleadosToRemove)
                {
                    empleados.Remove(empleadoToRemove);
                }
            }
            var response = _mapper.Map<List<GetAllEmpleadosResponseDTO>>(empleados);

            return response;
        }
    }
}
