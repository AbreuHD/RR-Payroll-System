using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Nacionalidad.Commands.CreateNacionalidad
{
    public class CreateNacionalidadCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
    }
    public class CreateNacionalidadCommandHandler : IRequestHandler<CreateNacionalidadCommand, bool>
    {
        private readonly INacionalidadRepository _nacionalidadRepository;
        private readonly IMapper _mapper;

        public CreateNacionalidadCommandHandler(INacionalidadRepository nacionalidadRepository, IMapper mapper)
        {
            _nacionalidadRepository = nacionalidadRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateNacionalidadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var nacionalidad = _mapper.Map<Domain.Entities.Nacionalidad>(request);
                await _nacionalidadRepository.AddAsync(nacionalidad);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
