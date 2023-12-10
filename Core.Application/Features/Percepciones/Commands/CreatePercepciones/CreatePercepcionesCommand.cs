using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Percepciones.Commands.CreatePercepciones
{
    public class CreatePercepcionesCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
    public class CreatePercepcionesCommandHandler : IRequestHandler<CreatePercepcionesCommand, bool>
    {
        private readonly IPercepcionesRepository _percepcionesRepository;
        private readonly IMapper _mapper;

        public CreatePercepcionesCommandHandler(IPercepcionesRepository percepcionesRepository, IMapper mapper)
        {
            _percepcionesRepository = percepcionesRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePercepcionesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.Percepciones>(request);
                await _percepcionesRepository.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
