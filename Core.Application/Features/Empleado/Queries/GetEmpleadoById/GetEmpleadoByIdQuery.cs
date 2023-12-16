using AutoMapper;
using Core.Application.DTOs.Empleados;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Empleado.Queries.GetEmpleadoById
{
    public class GetEmpleadoByIdQuery : IRequest<GetEmpleadoByID>
    {
        public int Id { get; set;}
    }
    public class GetEmpleadoByIdQueryHandler : IRequestHandler<GetEmpleadoByIdQuery, GetEmpleadoByID>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public GetEmpleadoByIdQueryHandler(IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public async Task<GetEmpleadoByID> Handle(GetEmpleadoByIdQuery request, CancellationToken cancellationToken)
        {
            var responseEmpleado = await _empleadoRepository.GetByIdAsync(request.Id);
            var response = _mapper.Map<GetEmpleadoByID>(responseEmpleado);
            return response;
        }
    }
}
