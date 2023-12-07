using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Asistencia.Command.CreateAsistenciaTable
{
    public class CreateAsistenciaTableCommand : IRequest<bool>
    {
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdEmpleadoProyecto { get; set; }
    }
    public class CreateAsistenciaTableCommandHander : IRequestHandler<CreateAsistenciaTableCommand, bool>
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMapper _mapper;

        public CreateAsistenciaTableCommandHander(IAsistenciaRepository asistenciaRepository, IMapper mapper)
        {
            _asistenciaRepository = asistenciaRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateAsistenciaTableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _mapper.Map<Core.Domain.Entities.Asistencia>(request);
                await _asistenciaRepository.AddAsync(response);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
