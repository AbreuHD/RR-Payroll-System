using AutoMapper;
using Core.Application.DTOs.EmpleadoProyecto;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.EmpleadoProyecto.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<List<EmpleadoProyectoReponseDTO>>
    {
        public string IdUser{ get; set; }
    }
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<EmpleadoProyectoReponseDTO>>
    {
        private readonly IEmpleadoProyectosRepository _empleadoProyectoRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IEmpleadoProyectosRepository empleadoProyectoRepository, IMapper mapper, IEmpleadoRepository empleadoRepository, IProyectoRepository proyectoRepository)
        {
            _empleadoProyectoRepository = empleadoProyectoRepository;
            _mapper = mapper;
            _empleadoRepository = empleadoRepository;
            _proyectoRepository = proyectoRepository;
        }

        public async Task<List<EmpleadoProyectoReponseDTO>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var empleado = await _empleadoRepository.GetAllAsync();
                var searchEmpleado = empleado.Where(x => x.UserID == request.IdUser).FirstOrDefault();

                var result = await _empleadoProyectoRepository.GetAllAsync();
                var search = result.Where(x => x.IdEmpleado == searchEmpleado.Id).ToList();
                var response = _mapper.Map<List<EmpleadoProyectoReponseDTO>>(search);


                foreach (var item in response)
                {
                    item.Proyecto = await _proyectoRepository.GetByIdAsync(item.IdProyecto);
                }
                if (response.Count == 0)
                {
                    response.Add(new EmpleadoProyectoReponseDTO() {
                        Empleado = searchEmpleado,
                        });
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener los proyectos", ex);
            }
        }
    }
}
