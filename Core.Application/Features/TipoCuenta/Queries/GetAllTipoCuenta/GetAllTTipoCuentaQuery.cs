using AutoMapper;
using Core.Application.DTOs.Banco;
using Core.Application.DTOs.TipoCuenta;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Banco.Queries.GetAllTipoBanco
{
    public class GetAllTTipoCuentaQuery : IRequest<List<TipoCuentaResponseDTO>>
    {
    }
    public class GetAllTTipoCuentaQueryHandler : IRequestHandler<GetAllTTipoCuentaQuery, List<TipoCuentaResponseDTO>>
    {
        private readonly ITipoCuentaRepository _tipoCuentaRepository;
        private readonly IMapper _mapper;
        
        public GetAllTTipoCuentaQueryHandler(ITipoCuentaRepository tipoCuentaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tipoCuentaRepository = tipoCuentaRepository;
        }

        public async Task<List<TipoCuentaResponseDTO>> Handle(GetAllTTipoCuentaQuery request, CancellationToken cancellationToken)
        {
            var result = await _tipoCuentaRepository.GetAllAsync();
            return _mapper.Map<List<TipoCuentaResponseDTO>>(result);
        }
    }
}
