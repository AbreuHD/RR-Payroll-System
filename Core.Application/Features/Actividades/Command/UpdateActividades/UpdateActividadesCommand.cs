using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Actividades.Command.UpdateActividades
{
    public class UpdateActividadesCommand : IRequest<bool>
    {
        public int UpdateID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
    public class UpdateActividadesCommandHandler : IRequestHandler<UpdateActividadesCommand, bool>
    {
        private readonly IActividadesRepository _actividadesRepository;
        private readonly IMapper _mapper;

        public UpdateActividadesCommandHandler(IActividadesRepository actividadesRepository, IMapper mapper)
        {
            _actividadesRepository = actividadesRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateActividadesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var actRequest = _mapper.Map<Domain.Entities.Actividades>(request);
                await _actividadesRepository.UpdateAsync(actRequest, request.UpdateID);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}