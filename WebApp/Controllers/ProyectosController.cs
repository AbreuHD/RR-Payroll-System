using Core.Application.DTOs.Proyecto;
using Core.Application.Features.Estado.Queries.GetAllStatus;
using Core.Application.Features.Proyecto.Queries.GetAllProjects;
using Core.Application.Features.Proyecto.Commands.CreateProject;
using Core.Application.Interface.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Features.Proyecto.Queries.GetProjectById;

namespace WebApp.Controllers
{
    public class ProyectosController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IActionResult> Index()
        {
            ViewBag.Estados = await Mediator.Send(new GetAllStatusQuery());
            return View(await Mediator.Send(new GetAllProjectsQuery()));
        }

        public async Task<IActionResult> Info(int Id)
        {
            return View(await Mediator.Send(new GetProjectByIdQuery
            {
                Id = Id
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditarProyectoDTO request)
        {
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
