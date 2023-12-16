using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Permiso.Command.CrearPermiso
{
    public class CrearPermisoCommand : IRequest<bool>
    {
        public DateTime FechaInicio { get; set; }
        public String Motivo { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Pagado { get; set; }
        public int IdAsistencia { get; set; }
    }
    public class CrearPermisoCommandHandler : IRequestHandler<CrearPermisoCommand, bool>
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IMapper _mapper;

        public CrearPermisoCommandHandler(IPermisoRepository permisoRepository, IMapper mapper)
        {
            _permisoRepository = permisoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CrearPermisoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var requestMap = _mapper.Map<Domain.Entities.Permiso>(request);
                await _permisoRepository.AddAsync(requestMap);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
