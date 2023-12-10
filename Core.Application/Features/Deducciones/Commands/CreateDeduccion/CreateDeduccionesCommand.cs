using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Deducciones.Commands.CreateDeduccion
{
    public class CreateDeduccionesCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
    public class CreateDeduccionesCommandHandler : IRequestHandler<CreateDeduccionesCommand, bool>
    {
        private readonly IDeduccionesRepository _deduccionesRepository;
        private readonly IMapper _mapper;
        
        public CreateDeduccionesCommandHandler(IDeduccionesRepository deduccionesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _deduccionesRepository = deduccionesRepository;
        }
        public async Task<bool> Handle(CreateDeduccionesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deducciones = _mapper.Map<Domain.Entities.Deducciones>(request);
                await _deduccionesRepository.AddAsync(deducciones);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
