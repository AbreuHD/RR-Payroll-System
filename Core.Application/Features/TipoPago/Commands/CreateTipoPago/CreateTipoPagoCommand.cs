using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.TipoPago.Commands.CreateTipoPago
{
    public class CreateTipoPagoCommand : IRequest<bool>
    {
        public string Cuenta { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdTipoBanco { get; set; }
        public string IdUsuario { get; set; }
    }

    public class CreateTipoPagoCommandHandler : IRequestHandler<CreateTipoPagoCommand, bool>
    {
        private readonly ITipoPagoRepository _tipoPagoRepository;
        private readonly IMapper _mapper;

        public CreateTipoPagoCommandHandler(ITipoPagoRepository tipoPagoRepository, IMapper mapper)
        {
            _tipoPagoRepository = tipoPagoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateTipoPagoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tipoPago = _mapper.Map<Core.Domain.Entities.TipoPago>(request);
                await _tipoPagoRepository.AddAsync(tipoPago);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
