using AutoMapper;
using Core.Application.DTOs.Actividades;
using Core.Application.Interface.Repository;
using MediatR;

namespace Core.Application.Features.Actividades.Queries.GetAllActividades
{
    public class GetAllActividadesQuery : IRequest<List<GetAllActividadesResponseDTO>>
    {
    }
    public class GetAllActividadesQueryHandler : IRequestHandler<GetAllActividadesQuery, List<GetAllActividadesResponseDTO>>
    {
        private readonly IActividadesRepository _actividadesRepository;
        private readonly IMapper _mapper;
        private readonly IActividadesAsignadasRepository _actividadesAsignadasRepository;

        public GetAllActividadesQueryHandler(IActividadesRepository actividadesRepository, IMapper mapper, IActividadesAsignadasRepository actividadesAsignadasRepository)
        {
            _actividadesRepository = actividadesRepository;
            _mapper = mapper;
            _actividadesAsignadasRepository = actividadesAsignadasRepository;
        }
        public async Task<List<GetAllActividadesResponseDTO>> Handle(GetAllActividadesQuery request, CancellationToken cancellationToken)
        {
            var actividades = await _actividadesRepository.GetAllAsync();
            var actividadesAsignadas = await _actividadesAsignadasRepository.GetAllAsync();
            var response = _mapper.Map<List<GetAllActividadesResponseDTO>>(actividades);

            foreach (var item in response)
            {
                foreach(var item2 in actividadesAsignadas)
                {
                    if(item.Id == item2.IdActividad)
                    {
                        item.HaveEmployee = true;
                    }
                }
            }
            return response;
        }
    }
}
