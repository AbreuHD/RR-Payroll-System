using AutoMapper;
using Core.Application.DTOs.Banco;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Banco.Queries.GetAllTipoBanco
{
    public class GetAllTipoBancoQuery : IRequest<List<TipoBancoResponseDTO>>
    {
    }
    public class GetAllTipoBancoQueryHandler : IRequestHandler<GetAllTipoBancoQuery, List<TipoBancoResponseDTO>>
    {
        private readonly ITipoBancoRepository _tipoBancoRepository;
        private readonly IMapper _mapper;
        
        public GetAllTipoBancoQueryHandler(ITipoBancoRepository tipoBancoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tipoBancoRepository = tipoBancoRepository;
        }

        public async Task<List<TipoBancoResponseDTO>> Handle(GetAllTipoBancoQuery request, CancellationToken cancellationToken)
        {
            var result = await _tipoBancoRepository.GetAllAsync();
            return _mapper.Map<List<TipoBancoResponseDTO>>(result);
        }
    }
}
