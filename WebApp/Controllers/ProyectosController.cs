using Core.Application.DTOs.Proyecto;
using Core.Application.Features.Estado.Queries.GetAllEstado;
using Core.Application.Features.Proyecto.Queries.GetAllProjects;
using Core.Application.Features.Proyecto.Commands.CreateProject;
using Core.Application.Interface.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Features.Proyecto.Queries.GetProjectById;
using Core.Application.Features.Puesto.Queries.GetAllPuestos;
using Core.Application.Features.Empleado.Queries.GetAllEmpleados;
using Core.Application.Features.EmpleadoProyecto.Commands.CreateEmpleadoProyecto;
using Core.Application.Features.EmpleadoProyecto.Commands.EditEmpleadoProyecto;
using Core.Application.Features.Proyecto.Commands.EditProject;
using Core.Application.Features.Asistencia.Command.CreateAsistenciaTable;

namespace WebApp.Controllers
{
    public class ProyectosController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IActionResult> Index()
        {
            ViewBag.Estados = await Mediator.Send(new GetAllEstadoQuery());
            return View(await Mediator.Send(new GetAllProjectsQuery()));
        }

        public async Task<IActionResult> Info(int Id)
        {
            ViewBag.Puestos = await Mediator.Send(new GetAllPuestosQuery());
            ViewBag.Empleados = await Mediator.Send(new GetAllEmpleadosSinProyecto());
            var response = (await Mediator.Send(new GetProjectByIdQuery {Id = Id} ));
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmpleado(CreateEmpleadoProyectoCommand request)
        {
            var response = await Mediator.Send(request);
            var asistenciaResponse = await Mediator.Send(new CreateAsistenciaTableCommand()
            {
                FechaEntrada = DateTime.UtcNow,
                FechaSalida = DateTime.UtcNow,
                IdEmpleadoProyecto = response.Id
            });

            return RedirectToAction("Info", new { Id = request.IdProyecto });
        }
        [HttpPost]
        public async Task<IActionResult> EditEmpleado(EditEmpleadoProyectoCommand request)
        {
            await Mediator.Send(request);
            return RedirectToAction("Info", new { Id = request.IdProyecto });
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectCommand request)
        {
            await Mediator.Send(request);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearProyectoDTO request)
        {
            var response = await Mediator.Send(new CreateProjectCommand
            {
                Cliente = request.Cliente,
                Descripcion = request.Descripcion,
                FechaFinal = request.FechaFinal,
                FechaInicio = request.FechaInicio,
                IdEstado = request.IdEstado,
                Locacion = request.Locacion,
                Nombre = request.Nombre
            });
            return RedirectToAction("Index");
        }
    }
}
