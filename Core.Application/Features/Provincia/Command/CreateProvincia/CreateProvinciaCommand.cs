using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Provincia.Command.CreateProvincia
{
    public class CreateProvinciaCommand : IRequest<bool>
    {
        public string Nombre { get; set; }
    }
    public class CreateProvinciaCommandHandler : IRequestHandler<CreateProvinciaCommand, bool>
    {
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly IMapper _mapper;

        public CreateProvinciaCommandHandler(IProvinciaRepository provinciaRepository, IMapper mapper)
        {
            _provinciaRepository = provinciaRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateProvinciaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var provincia = _mapper.Map<Domain.Entities.Provincia>(request);
                await _provinciaRepository.AddAsync(provincia);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
