using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Estado.Command.CreateEstado
{
    public class CreateEstadoCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class CreateEstadoCommandHandler : IRequestHandler<CreateEstadoCommand, bool>
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMapper _mapper;

        public CreateEstadoCommandHandler(IEstadoRepository estadoRepository, IMapper mapper)
        {
            _estadoRepository = estadoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateEstadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var estado = _mapper.Map<Domain.Entities.Estado>(request);
                await _estadoRepository.AddAsync(estado);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
