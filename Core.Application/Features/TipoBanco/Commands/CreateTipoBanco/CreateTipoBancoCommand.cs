using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Banco.Commands.CreateBanco
{
    public class CreateTipoBancoCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
    }
    public class CreateBancoCommandHandler : IRequestHandler<CreateTipoBancoCommand, bool>
    {
        private readonly ITipoBancoRepository _tipoBancoRepository;
       
        public CreateBancoCommandHandler(ITipoBancoRepository tipoBancoRepository)
        {
            _tipoBancoRepository = tipoBancoRepository;
        }

        public async Task<bool> Handle(CreateTipoBancoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _tipoBancoRepository.AddAsync(new TipoBanco
                {
                    Nombre = request.Nombre
                });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
    }
}
