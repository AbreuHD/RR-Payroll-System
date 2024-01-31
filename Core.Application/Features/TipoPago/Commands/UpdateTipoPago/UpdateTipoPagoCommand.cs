using AutoMapper;
using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.TipoPago.Commands.UpdateTipoPago
{
    public class UpdateTipoPagoCommand : IRequest<bool>
    {
        public int IdTipoPago { get; set; }
        public string Cuenta { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdTipoBanco { get; set; }
        public string IdUsuario { get; set; }
    }

    public class UpdateTipoPagoCommandHandler : IRequestHandler<UpdateTipoPagoCommand, bool>
    {
        private readonly ITipoPagoRepository _tipoPagoRepository;
        private readonly IMapper _mapper;

        public UpdateTipoPagoCommandHandler(ITipoPagoRepository tipoPagoRepository, IMapper mapper)
        {
            _tipoPagoRepository = tipoPagoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateTipoPagoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingTipoPago = await _tipoPagoRepository.GetByIdAsync(request.IdTipoPago);
                _mapper.Map(request, existingTipoPago);
                await _tipoPagoRepository.UpdateAsync(existingTipoPago, request.IdTipoPago);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
