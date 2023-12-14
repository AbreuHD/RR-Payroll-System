using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Empleado.Queries.GetEmpleadoByIdentityId
{
    public class GetEmpleadoByIdentityIdQuery : IRequest<int>
    {
        public string IdentityId { get; set; }
    }
    public class GetEmpleadoByIdentityIdQueryHandler : IRequestHandler<GetEmpleadoByIdentityIdQuery, int>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        public GetEmpleadoByIdentityIdQueryHandler(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }
        public async Task<int> Handle(GetEmpleadoByIdentityIdQuery request, CancellationToken cancellationToken)
        {
            var empleado = await _empleadoRepository.GetAllAsync();
            var empleadoResult = empleado.FirstOrDefault(x => x.UserID == request.IdentityId);
            return empleadoResult != null ? empleadoResult.Id : 0;
        }
    }
}
