using AutoMapper;
using Core.Application.DTOs.Actividades;
using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Actividades.Queries.GetActivityById
{
    public class GetActivityByIdQuery : IRequest<GetActivityByIdResponseDTO>
    {
        public int Id { get; set;}
    }
    public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, GetActivityByIdResponseDTO>
    {
        private readonly IActividadesRepository _actividadesRepository;
        private readonly IActividadesAsignadasRepository _actividadesAsignadasRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public GetActivityByIdQueryHandler(IActividadesRepository actividadesRepository, IActividadesAsignadasRepository actividadesAsignadasRepository, IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _actividadesRepository = actividadesRepository;
            _actividadesAsignadasRepository = actividadesAsignadasRepository;
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public async Task<GetActivityByIdResponseDTO> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var actividadesAsignadas = await _actividadesAsignadasRepository.GetAllAsync();
            var actObjetive = actividadesAsignadas.Where(x => x.IdActividad == request.Id).ToList();
            
            var response = _mapper.Map<GetActivityByIdResponseDTO>(await _actividadesRepository.GetByIdAsync(request.Id));

            List<Domain.Entities.Empleado> empleadosResponse = new();

            foreach (var item in actObjetive)
            {
                empleadosResponse.Add(await _empleadoRepository.GetByIdAsync(item.IdEmpleadoProyecto));
            }
            response.Empleados = empleadosResponse;


            return response;
        }
    }
}
