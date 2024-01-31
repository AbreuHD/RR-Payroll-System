using Core.Application.DTOs.Register;
using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.TipoPago.Queries.GetTipoPagoByIdentityId
{
    public class GetTipoPagoByIdentityIdQuery : IRequest<RegisterRequestDTO>
    {
        public string IdentityId { get; set; }
        public RegisterRequestDTO Data { get; set; }
    }
    public class GetTipoPagoByIdentityIdQueryHandler : IRequestHandler<GetTipoPagoByIdentityIdQuery, RegisterRequestDTO>
    {
        private readonly ITipoPagoRepository _tipoPagoRepository;
        public GetTipoPagoByIdentityIdQueryHandler(ITipoPagoRepository tipoPagoRepository)
        {
            _tipoPagoRepository = tipoPagoRepository;
        }
        public async Task<RegisterRequestDTO> Handle(GetTipoPagoByIdentityIdQuery request, CancellationToken cancellationToken)
        {
            var tipoList = await _tipoPagoRepository.GetAllAsync();
            var tipoPago = tipoList.FirstOrDefault(x => x.IdUsuario == request.IdentityId);

            request.Data.Cuenta = tipoPago.Cuenta;
            request.Data.TipoCuenta = tipoPago.IdTipoCuenta;
            request.Data.Banco = tipoPago.IdTipoBanco;
            request.Data.IdTipoPago = tipoPago.Id;

            return request.Data;
        }
    }
}
