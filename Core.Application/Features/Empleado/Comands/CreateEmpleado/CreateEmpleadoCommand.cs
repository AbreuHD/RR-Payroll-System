using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Empleado.Comands.CreateEmpleado
{
    public class CreateEmpleadoCommand : IRequest<bool>
    {
        public string UserID { get; set; }
        public string Documento { get; set; }
        public int Codigo { get; set; }
        public string NumCuenta { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Sexo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string tipoPago { get; set; }

        public int IdNacionalidad { get; set; }
        public int IdProvincia { get; set; }
        public int IdEstado { get; set; }
        public int IdLicencia { get; set; }
    }

    public class CreateEmpleadoCommandHandler : IRequestHandler<CreateEmpleadoCommand, bool>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public CreateEmpleadoCommandHandler(IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateEmpleadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var empleado = _mapper.Map<Domain.Entities.Empleado>(request);
                await _empleadoRepository.AddAsync(empleado);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
