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
    }
    public class GetAllEmpleadosWithoutChosenQueryHandler : IRequestHandler<GetAllEmpleadosWithoutChosenQuery, List<GetAllEmpleadosResponseDTO>>>
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

            foreach(var i in requestEmpleados)
            {
                foreach(var j in request.Empleados)
                {
                    if(i.Id == j.Id)
                    {
                        requestEmpleados.Remove(i);
                    }
                }
            }
            var response = _mapper.Map<List<GetAllEmpleadosResponseDTO>>(requestEmpleados);

            return response;
        }
    }
}
