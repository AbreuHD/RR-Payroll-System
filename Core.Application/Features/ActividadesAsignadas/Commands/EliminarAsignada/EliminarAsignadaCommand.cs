using Core.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.ActividadesAsignadas.Commands.EliminarAsignada
{
    public class EliminarAsignadaCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
    }
    public class EliminarAsignadaCommandHandler : IRequestHandler<EliminarAsignadaCommand, bool>
    {
        private readonly IActividadesAsignadasRepository _actividadesAsignadasRepository;
        private readonly IEmpleadoProyectosRepository _empleadoProyectosRepository;
        
        public EliminarAsignadaCommandHandler(IActividadesAsignadasRepository actividadesAsignadasRepository, IEmpleadoProyectosRepository empleadoProyectosRepository)
        {
            _actividadesAsignadasRepository = actividadesAsignadasRepository;
            _empleadoProyectosRepository = empleadoProyectosRepository;
        }
        public async Task<bool> Handle(EliminarAsignadaCommand request, CancellationToken cancellationToken)
        {
            var entities = await _actividadesAsignadasRepository.GetAllAsync();
            var empleadoProyectos = await _empleadoProyectosRepository.GetAllAsync();
            var delId = 0;
            //foreach (var item in empleadoProyectos)
            //{
            //    if (item.IdEmpleado == request.IdUser)
            //    {
            //        delId = item.Id;
            //    }
            //}

            foreach (var item in entities)
            {
                if (item.IdActividad == request.Id && item.IdEmpleadoProyecto == request.IdUser)
                {
                    await _actividadesAsignadasRepository.DeleteAsync(item);
                    return true;
                }
            }
            return false;

        }
    }
}
