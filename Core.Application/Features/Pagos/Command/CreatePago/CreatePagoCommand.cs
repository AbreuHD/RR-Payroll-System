using AutoMapper;
using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Pagos.Command.CreatePago
{
    public class CreatePagoCommand : IRequest<bool>
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        public string Emisor { get; set; }
        public int IdTipoPago { get; set; }
        public int IdEmpleado { get; set; }
        public int IdPercepciones { get; set; }
        public int IdDeducciones { get; set; }
    }
    public class CreatePagoCommandHandler : IRequestHandler<CreatePagoCommand, bool>
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IMapper _mapper;

        public CreatePagoCommandHandler(IPagoRepository pagoRepository, IMapper mapper)
        {
            _pagoRepository = pagoRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreatePagoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _mapper.Map<Pago>(request);
                await _pagoRepository.AddAsync(response);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }   
}
