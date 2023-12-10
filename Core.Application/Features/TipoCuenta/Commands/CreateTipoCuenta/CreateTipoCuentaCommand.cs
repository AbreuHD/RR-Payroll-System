using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.TipoCuenta.Commands.CreateTipoCuenta
{
    public class CreateTipoCuentaCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
    }
    public class CreateTipoCuentaCommandHandler : IRequestHandler<CreateTipoCuentaCommand, bool>
    {
        private readonly ITipoCuentaRepository _tipoCuentaRepository;
       
        public CreateTipoCuentaCommandHandler(ITipoCuentaRepository tipoCuentaRepository)
        {
            _tipoCuentaRepository = tipoCuentaRepository;
        }

        public async Task<bool> Handle(CreateTipoCuentaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _tipoCuentaRepository.AddAsync(new Core.Domain.Entities.TipoCuenta
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
