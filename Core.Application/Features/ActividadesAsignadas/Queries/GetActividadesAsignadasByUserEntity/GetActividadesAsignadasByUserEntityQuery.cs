using AutoMapper;
using Core.Application.DTOs.ActividadesAsignadas;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.ActividadesAsignadas.Queries.GetActividadesAsignadasByUserEntity
{
    public class GetActividadesAsignadasByUserEntityQuery : IRequest<List<GetActividadesAsignadasByUserResponseDTO>>
    {
        public string IdUser { get; set;}
        public int ProyectoId { get; set;}
    }
    public class GetActividadesAsignadasByUserEntityQueryHandler : IRequestHandler<GetActividadesAsignadasByUserEntityQuery, List<GetActividadesAsignadasByUserResponseDTO>>
    {
        private readonly IActividadesAsignadasRepository _actividadesAsignadasRepository;
        private readonly IActividadesRepository _actividadesRepository;
        private readonly IMapper _mapper;
        private readonly IEmpleadoRepository _empleadoRepository;
        public GetActividadesAsignadasByUserEntityQueryHandler(IActividadesAsignadasRepository actividadesAsignadasRepository, IActividadesRepository actividadesRepository, IMapper mapper, IEmpleadoRepository empleadoRepository)
        {
            _actividadesAsignadasRepository = actividadesAsignadasRepository;
            _actividadesRepository = actividadesRepository;
            _mapper = mapper;
            _empleadoRepository = empleadoRepository;
        }
        public async Task<List<GetActividadesAsignadasByUserResponseDTO>> Handle(GetActividadesAsignadasByUserEntityQuery request, CancellationToken cancellationToken)
        {
            var empleado = await _empleadoRepository.GetAllAsync();
            var empleadoSel = empleado.Where(x => x.UserID == request.IdUser).FirstOrDefault();

            var actividadesAsignadas = await _actividadesAsignadasRepository.GetAllWithIncludes(new List<string> { "Actividades", "EmpleadoProyectos" });
            //var actividadesUser = actividadesAsignadas.Where(x => x.PRO == request.ProyectoId).ToList();
            //foreach(var i in actividadesUser)
            //{
            //    i.Actividad = await _actividadesRepository.GetByIdAsync(i.IdActividad);
            //}
            var response = _mapper.Map<List<GetActividadesAsignadasByUserResponseDTO>>(empleadoSel);
            return response;
        }
    }
}
