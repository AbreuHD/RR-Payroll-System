using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Horas.Command.AddHoras
{
    public class AddHorasCommand : IRequest<bool>
    {
        public int IdEmpleadoProyecto { get; set; }
    }
    public class AddAsistenciaCommandHandler : IRequestHandler<AddHorasCommand, bool>
    {
        private readonly IHorasRepository _horasRepository;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMapper _mapper;

        public AddAsistenciaCommandHandler(IHorasRepository horasRepository, IMapper mapper, IAsistenciaRepository asistenciaRepository)
        {
            _horasRepository = horasRepository;
            _mapper = mapper;
            _asistenciaRepository = asistenciaRepository;
        }

        public async Task<bool> Handle(AddHorasCommand request, CancellationToken cancellationToken)
        {
            try
            {   
                var asistencia = await _asistenciaRepository.GetAllAsync();
                var search = asistencia.Where(x => x.IdEmpleadoProyecto == request.IdEmpleadoProyecto).FirstOrDefault();

               // var asistencia = _mapper.Map<Domain.Entities.Horas>(request);
                var hours = await _horasRepository.GetAllAsync();
                var results = hours.Where(x => x.IdAsistencia == search.Id).Where(x => x.HorasInicio.Day == DateTime.Today.Day).FirstOrDefault();
                if (results != null)
                {
                    results.HoraFinal = DateTime.Now;
                    await _horasRepository.UpdateAsync(results, results.Id);
                    return true;
                }
                else
                {
                    await _horasRepository.AddAsync(new Domain.Entities.Horas()
                    {
                        IdAsistencia = search.Id,
                        HorasInicio = DateTime.Now
                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo agregar la asistencia", ex);
                return false ;

            }
        }
    }
}
