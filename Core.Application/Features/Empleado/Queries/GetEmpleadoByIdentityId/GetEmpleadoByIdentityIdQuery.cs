using Core.Application.DTOs.Register;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Empleado.Queries.GetEmpleadoByIdentityId
{
    public class GetEmpleadoByIdentityIdQuery : IRequest<RegisterRequestDTO>
    {
        public string IdentityId { get; set; }
        public RegisterRequestDTO? Data { get; set; }
    }
    public class GetEmpleadoByIdentityIdQueryHandler : IRequestHandler<GetEmpleadoByIdentityIdQuery, RegisterRequestDTO>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        public GetEmpleadoByIdentityIdQueryHandler(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }
        public async Task<RegisterRequestDTO> Handle(GetEmpleadoByIdentityIdQuery request, CancellationToken cancellationToken)
        {
            var empleado = await _empleadoRepository.GetAllAsync();
            var empleadoResult = empleado.FirstOrDefault(x => x.UserID == request.IdentityId);
            
            if(request.Data == null)
            {
                request.Data = new RegisterRequestDTO();
            }

            request.Data.Nombre = empleadoResult.Nombre;
            request.Data.Apellido = empleadoResult.Apellido;
            request.Data.Cedula = empleadoResult.Cedula;
            request.Data.Nacimiento = empleadoResult.FechaNacimiento;
            request.Data.Telefono = empleadoResult.Telefono;
            request.Data.Direccion = empleadoResult.Direccion;
            request.Data.Provincia = empleadoResult.IdProvincia;
            request.Data.Nacionalidad = empleadoResult.IdNacionalidad;
            request.Data.Genero = empleadoResult.Sexo ? 1 : 0;
            request.Data.Estado = empleadoResult.IdEstado;
            request.Data.Correo = empleadoResult.Email;
            request.Data.Direccion = empleadoResult.Direccion;
            request.Data.IdEmpleado = empleadoResult.Id;
            return request.Data;
        }
    }
}
