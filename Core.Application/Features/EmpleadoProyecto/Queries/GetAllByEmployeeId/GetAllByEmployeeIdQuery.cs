using AutoMapper;
using Core.Application.DTOs.EmpleadoProyecto;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.EmpleadoProyecto.Queries.GetAllByEmployeeId
{
    public class GetAllByEmployeeIdQuery : IRequest<List<GetAllByEmployeeIdDTO>>
    {
        public int IdUser { get; set; }
    }
    public class GetAllByEmployeeIdQueryHandler : IRequestHandler<GetAllByEmployeeIdQuery, List<GetAllByEmployeeIdDTO>>
    {
        private readonly IEmpleadoProyectosRepository _empleadoProyectosRepository;
        private readonly IMapper _mapper;
        public GetAllByEmployeeIdQueryHandler(IEmpleadoProyectosRepository empleadoProyectosRepository, IMapper mapper)
        {
            _empleadoProyectosRepository = empleadoProyectosRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllByEmployeeIdDTO>> Handle(GetAllByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var responseRepo = await _empleadoProyectosRepository.GetAllWithIncludes(new List<string> { "Proyecto" });
            var responseChoose = responseRepo.Where(x => x.IdEmpleado == request.IdUser).ToList();
            List<GetAllByEmployeeIdDTO> response = new List<GetAllByEmployeeIdDTO>();
            foreach (var item in responseChoose)
            {
                response.Add(new GetAllByEmployeeIdDTO
                {
                    Id = item.Id,
                    Nombre = item.Proyecto.Nombre
                });
            }

            return response;
        }
    }
}
