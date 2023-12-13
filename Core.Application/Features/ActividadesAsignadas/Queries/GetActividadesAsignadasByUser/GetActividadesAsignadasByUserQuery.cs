using AutoMapper;
using Core.Application.DTOs.ActividadesAsignadas;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.ActividadesAsignadas.Queries.GetActividadesAsignadasByUserQuery
{
    public class GetActividadesAsignadasByUserQuery : IRequest<List<GetActividadesAsignadasByUserResponseDTO>>
    {
        public int IdEmpleadoProyecto { get; set;}
    }
    public class GetActividadesAsignadasByUserQueryHandler : IRequestHandler<GetActividadesAsignadasByUserQuery, List<GetActividadesAsignadasByUserResponseDTO>>
    {
        private readonly IActividadesAsignadasRepository _actividadesAsignadasRepository;
        private readonly IActividadesRepository _actividadesRepository;
        private readonly IMapper _mapper;

        public GetActividadesAsignadasByUserQueryHandler(IActividadesAsignadasRepository actividadesAsignadasRepository, IActividadesRepository actividadesRepository, IMapper mapper)
        {
            _actividadesAsignadasRepository = actividadesAsignadasRepository;
            _actividadesRepository = actividadesRepository;
            _mapper = mapper;
        }
        public async Task<List<GetActividadesAsignadasByUserResponseDTO>> Handle(GetActividadesAsignadasByUserQuery request, CancellationToken cancellationToken)
        {
            var actividadesAsignadas = await _actividadesAsignadasRepository.GetAllAsync();
            var actividadesUser = actividadesAsignadas.Where(x => x.IdEmpleadoProyecto == request.IdEmpleadoProyecto).ToList();
            foreach(var i in actividadesUser)
            {
                i.Actividad = await _actividadesRepository.GetByIdAsync(i.IdActividad);
            }
            var response = _mapper.Map<List<GetActividadesAsignadasByUserResponseDTO>>(actividadesUser);
            return response;
        }
    }
}
