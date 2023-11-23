using AutoMapper;
using Core.Application.DTOs.Nacionalidad;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Nacionalidad.Queries.GetAllNacionalidad
{
    public class GetAllNacionalidadQuery : IRequest<List<GetAllNacionalidadResponseDTO>>
    {
    }
    public class GetAllNacionalidadQueryHandler : IRequestHandler<GetAllNacionalidadQuery, List<GetAllNacionalidadResponseDTO>>
    {
        private readonly INacionalidadRepository _nacionalidadRepository;
        private readonly IMapper _mapper;

        public GetAllNacionalidadQueryHandler(INacionalidadRepository nacionalidadRepository, IMapper mapper)
        {
            _nacionalidadRepository = nacionalidadRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllNacionalidadResponseDTO>> Handle(GetAllNacionalidadQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var nacionalidad = await _nacionalidadRepository.GetAllAsync();
                var nacionalidadDTO = _mapper.Map<List<GetAllNacionalidadResponseDTO>>(nacionalidad);
                return nacionalidadDTO;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
