using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Actividades.Command.CreateActividades
{
    public class CreateActividadesCommand : IRequest<bool>
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int IdProyecto { get; set; }
    }
    public class CreateActividadesCommandHandler : IRequestHandler<CreateActividadesCommand, bool>
    {
        private readonly IActividadesRepository _actividadesRepository;
        private readonly IMapper _mapper;
        public CreateActividadesCommandHandler(IActividadesRepository actividadesRepository, IMapper mapper)
        {
            _actividadesRepository = actividadesRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateActividadesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _actividadesRepository.AddAsync(_mapper.Map<Domain.Entities.Actividades>(request));
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
