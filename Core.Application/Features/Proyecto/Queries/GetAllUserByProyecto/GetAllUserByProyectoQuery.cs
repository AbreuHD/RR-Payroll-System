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

namespace Core.Application.Features.Proyecto.Queries.GetAllUserByProyecto
{
    public class GetAllUserByProyectoQuery : IRequest<List<GetAllEmpleadosResponseDTO>>
    {
        public int IdProyecto { get; set; }
    }
    public class GetAllUserByProyectoQueryHandler : IRequestHandler<GetAllUserByProyectoQuery, List<GetAllEmpleadosResponseDTO>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IEmpleadoProyectosRepository _empleadoProyectosRepository;
        private readonly IMapper _mapper;

        public GetAllUserByProyectoQueryHandler(IEmpleadoRepository empleadoRepository, IMapper mapper, IEmpleadoProyectosRepository empleadoProyectosRepository)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
            _empleadoProyectosRepository = empleadoProyectosRepository;
        }
        public async Task<List<GetAllEmpleadosResponseDTO>> Handle(GetAllUserByProyectoQuery request, CancellationToken cancellationToken)
        {
            //var empleados = await _empleadoRepository.GetAllWithIncludes(new List<string> { "EmpleadoProyectos" });
            var empleadoProyectos = await _empleadoProyectosRepository.GetAllWithIncludes(new List<string> { "Empleado" });
            
            List<Domain.Entities.Empleado> EmpleadoSel = new List<Domain.Entities.Empleado>();

                foreach (var item2 in empleadoProyectos)
                {
                    if(item2.IdProyecto == request.IdProyecto)
                    {
                        EmpleadoSel.Add(item2.Empleado);
                    }
                }


            var response = _mapper.Map<List<GetAllEmpleadosResponseDTO>>(EmpleadoSel);
            return response;
        }
    }
}
