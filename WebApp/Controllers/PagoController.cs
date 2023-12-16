using Core.Application.DTOs.Empleados;
using Core.Application.Features.Deducciones.Commands.CreateDeduccion;
using Core.Application.Features.Deducciones.Queries.GetAllDeducciones;
using Core.Application.Features.Deducciones.Queries.GetAllDeduccionesWithoutDefault;
using Core.Application.Features.Empleado.Queries.GetAllEmpleados;
using Core.Application.Features.EmpleadoProyecto.Queries.GetAllProjects;
using Core.Application.Features.Pagos.Command.CreatePago;
using Core.Application.Features.Pagos.Queries.GetPagoByUserID;
using Core.Application.Features.Percepciones.Commands.CreatePercepciones;
using Core.Application.Features.Percepciones.Queries.GetAllPercepcion;
using Core.Application.Features.Percepciones.Queries.GetAllPercepcionWithoutDefault;
using Core.Application.Features.Proyecto.Queries.GetAllUserByProyecto;
using Core.Application.Interface.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace WebApp.Controllers
{
    public class PagoController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private readonly IUserService _userService;
        public PagoController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await Mediator.Send(new Core.Application.Features.Proyecto.Queries.GetAllProjects.GetAllProjectsQuery());
            return View(response);
        }

        public async Task<IActionResult> Detalle(int Id)
        {
            ViewBag.Deducciones = await Mediator.Send(new GetAllDeduccionesWithoutDefaultQuery());
            ViewBag.Percepciones = await Mediator.Send(new GetAllPercepcionWithoutDefaultQuery());
            var response = await Mediator.Send(new GetAllUserByProyectoQuery() { IdProyecto = Id});
            ViewBag.IdProyecto = Id;
            return View(response);
        }

        public async Task<IActionResult> Deducciones()
        {
            var response = await Mediator.Send(new GetAllDeduccionesQuery());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeducciones(CreateDeduccionesCommand comm)
        {
            await Mediator.Send(comm);
            return RedirectToAction("Deducciones");
        }

        public async Task<IActionResult> Percepciones()
        {
            var response = await Mediator.Send(new GetAllPercepcionQuery());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePercepciones(CreatePercepcionesCommand comm)
        {
            await Mediator.Send(comm);
            return RedirectToAction("Percepciones");
        }

        [HttpPost]
        public async Task<IActionResult> CrearPago(CreatePagoCommand comm)
        {
            var response = await Mediator.Send(comm);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> HistoricoPago(int IdEmpleado, int IdProyecto)
        {
            var response = await Mediator.Send(new GetPagoByUserIDQuery() { 
                IdProyecto = IdProyecto,
                IdEmpleado = IdEmpleado
            });
            return View(response);
        }
    }
}
