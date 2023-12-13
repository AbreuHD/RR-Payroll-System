using Core.Application.Features.Actividades.Command.CreateActividades;
using Core.Application.Features.Actividades.Command.UpdateActividades;
using Core.Application.Features.Actividades.Queries.GetActivityById;
using Core.Application.Features.Actividades.Queries.GetAllActividades;
using Core.Application.Features.ActividadesAsignadas.Commands.CreateActAsignada;
using Core.Application.Features.ActividadesAsignadas.Queries.GetActividadesAsignadasByUserQuery;
using Core.Application.Features.Empleado.Queries.GetAllEmpleadosWithoutChosen;
using Core.Application.Features.Proyecto.Queries.GetAllProjects;
using Core.Application.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApp.Controllers
{ 
    public class ActividadesController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IActionResult> Index(int IdEmpleadoProyecto)
        {

            var response = await Mediator.Send(new GetActividadesAsignadasByUserQuery()
            {
                IdEmpleadoProyecto = IdEmpleadoProyecto
            });
            return View(response);
        }

        public async Task<IActionResult> Admin()
        {
            ViewBag.Proyectos = await Mediator.Send(new GetAllProjectsQuery());
            var response = await Mediator.Send(new GetAllActividadesQuery());
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTarea(CreateActividadesCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Admin");
        }

        public async Task<IActionResult> Info(int Id, int IdProyecto)
        {
            var response = await Mediator.Send(new GetActivityByIdQuery() { Id = Id });
            ViewBag.Empleados = await Mediator.Send(new GetAllEmpleadosWithoutChosenQuery() 
            { 
                Empleados = response.Empleados, 
                IdProyecto = IdProyecto
            });
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTarea(UpdateActividadesCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEmpleados(CreateActAsignadaCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Admin");
        }
    }
}