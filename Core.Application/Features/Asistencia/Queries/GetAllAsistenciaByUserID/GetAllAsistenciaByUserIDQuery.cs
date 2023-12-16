using AutoMapper;
using Core.Application.DTOs.Asistencia;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Asistencia.Queries.GetAllAsistenciaByUserID
{
    public class GetAllAsistenciaByUserIDQuery : IRequest<GetAllAsistenciaByUserIDResponse>
    {
        public int EmpleadoProyectoID { get; set; }
    }
    public class GetAllAsistenciaByUserIDQueryHandler : IRequestHandler<GetAllAsistenciaByUserIDQuery, GetAllAsistenciaByUserIDResponse>
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMapper _mapper;
        private readonly IEmpleadoProyectosRepository _empleadoProyectosRepository;
        private readonly IHorasRepository _horasRepository;
        private readonly IPermisoRepository _permisoRepository;

        public GetAllAsistenciaByUserIDQueryHandler(IAsistenciaRepository asistenciaRepository, IMapper mapper, IEmpleadoProyectosRepository empleadoProyectosRepository, IHorasRepository horasRepository, IPermisoRepository permisosRepository)
        {
            _asistenciaRepository = asistenciaRepository;
            _mapper = mapper;
            _empleadoProyectosRepository = empleadoProyectosRepository;
            _horasRepository = horasRepository;
            _permisoRepository = permisosRepository;
        }

        public async Task<GetAllAsistenciaByUserIDResponse> Handle(GetAllAsistenciaByUserIDQuery request, CancellationToken cancellationToken)
        {
            var responseEmpleadoProyecto = await _empleadoProyectosRepository.GetAllWithIncludes(new List<string> { "Asistencia" });
            var responseEmpleadoChoose = responseEmpleadoProyecto.Where(x => x.Id== request.EmpleadoProyectoID).FirstOrDefault();
            

            var horas = await _horasRepository.GetAllAsync();
            var permisos = await _permisoRepository.GetAllAsync();

            foreach (var item in horas)
            {
                if(item.IdAsistencia == responseEmpleadoChoose.Id)
                {
                    responseEmpleadoChoose.Asistencia.Horas.Add(item);
                }
            }
            foreach(var item in permisos)
            {
                if(item.IdAsistencia == responseEmpleadoChoose.Id)
                {
                    responseEmpleadoChoose.Asistencia.Permiso.Add(item);
                }
            }

            var response = _mapper.Map<GetAllAsistenciaByUserIDResponse>(responseEmpleadoChoose.Asistencia);
            response.Asistencia = new List<AsistenciaResponseDTO>();
            foreach (var x in response.Horas)
            {
                response.Asistencia.Add(new AsistenciaResponseDTO()
                {
                    Inicio = x.HorasInicio,
                    Final = x.HoraFinal,
                    IsPagado = true,
                    Source = "Ponche",
                    Id = x.Id
                });
            }
            foreach (var x in response.Permiso)
            {
                response.Asistencia.Add(new AsistenciaResponseDTO()
                {
                    Inicio = x.FechaInicio,
                    Final = x.FechaFinal,
                    IsPagado = x.Pagado,
                    Source = "Permiso",
                    Id = x.Id
                });
            }
            if(response.Asistencia != null)
            {
                response.Asistencia = response.Asistencia.OrderBy(x => x.Inicio).ToList();
            }
            return response;
        }
    }
}
