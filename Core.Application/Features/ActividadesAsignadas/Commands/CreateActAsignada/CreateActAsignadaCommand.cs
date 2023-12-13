using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.ActividadesAsignadas.Commands.CreateActAsignada
{
    public class CreateActAsignadaCommand : IRequest<bool>
    {
        public int IdActividad { get; set; }
        public int IdEmpleadoProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Estado { get; set; }
    }
    public class CreateActAsignadaCommandHandler : IRequestHandler<CreateActAsignadaCommand, bool>
    {
        private readonly IActividadesAsignadasRepository _actividadesAsignadasRepository;
        private readonly IMapper _mapper;

        public CreateActAsignadaCommandHandler(IActividadesAsignadasRepository actividadesAsignadasRepository, IMapper mapper)
        {
            _actividadesAsignadasRepository = actividadesAsignadasRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateActAsignadaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _mapper.Map<Domain.Entities.ActividadesAsignadas>(request);
                await _actividadesAsignadasRepository.AddAsync(response);
                return true;
            }catch(Exception e)
            {
                return false;
            }

        }
    }
}
