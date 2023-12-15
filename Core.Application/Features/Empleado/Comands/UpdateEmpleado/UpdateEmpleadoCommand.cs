using AutoMapper;
using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Empleado.Comands.UpdateEmpleado
{
    public class UpdateEmpleadoCommand : IRequest<bool>
    {
        public int IdOld { get; set; }
        public int IdEmpleado { get; set; }
        public string UserID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Sexo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int IdNacionalidad { get; set; }
        public int IdProvincia { get; set; }
        public int IdEstado { get; set; }
    }
    public class UpdatesEmpleadoCommandHandler : IRequestHandler<UpdateEmpleadoCommand, bool>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public async Task<bool> Handle(UpdateEmpleadoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var empleado = _mapper.Map<Domain.Entities.Empleado>(request);
                await _empleadoRepository.UpdateAsync(empleado, request.IdOld);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
