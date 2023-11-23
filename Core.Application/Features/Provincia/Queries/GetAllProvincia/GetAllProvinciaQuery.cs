using AutoMapper;
using Core.Application.DTOs.Provincia;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Provincia.Queries.GetAllProvincia
{
    public class GetAllProvinciaQuery : IRequest<List<ProvinciaResponseDTO>>
    {
    }
    public class GetAllProvinciaQueryHandler : IRequestHandler<GetAllProvinciaQuery, List<ProvinciaResponseDTO>>
    {
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly IMapper _mapper;

        public GetAllProvinciaQueryHandler(IProvinciaRepository provinciaRepository, IMapper mapper)
        {
            _provinciaRepository = provinciaRepository;
            _mapper = mapper;
        }

        public async Task<List<ProvinciaResponseDTO>> Handle(GetAllProvinciaQuery request, CancellationToken cancellationToken)
        {
            var provincia = await _provinciaRepository.GetAllAsync();
            var provinciaDTO = _mapper.Map<List<ProvinciaResponseDTO>>(provincia);
            return provinciaDTO;
        }
    }
}
