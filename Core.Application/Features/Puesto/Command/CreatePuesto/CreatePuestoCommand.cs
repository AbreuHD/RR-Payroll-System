using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Puesto.Command.CreatePuesto
{
    public class CreatePuestoCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class CreatePuestoCommandHandler : IRequestHandler<CreatePuestoCommand, bool>
    {
        private readonly IPuestoRepository _puestoRepository;
        private readonly IMapper _mapper;

        public CreatePuestoCommandHandler(IPuestoRepository puestoRepository, IMapper mapper)
        {
            _puestoRepository = puestoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePuestoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var puesto = _mapper.Map<Domain.Entities.Puesto>(request);
                await _puestoRepository.AddAsync(puesto);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
