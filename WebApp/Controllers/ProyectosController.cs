using Core.Application.DTOs.Proyecto;
using Core.Application.Features.Estado.Commands.GetAllStatus;
using Core.Application.Features.Proyecto.Commands.GetAllProjects;
using Core.Application.Features.Proyecto.Queries.CreateProject;
using Core.Application.Interface.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProyectosController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IActionResult> Index()
        {
            ViewBag.Estados = await Mediator.Send(new GetAllStatusCommand());
            return View(await Mediator.Send(new GetAllProjectsCommand()));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearProyectoDTO request)
        {
            var response = await Mediator.Send(new CreateProjectQuery
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
