using Core.Application.Features.Deducciones.Commands.CreateDeduccion;
using Core.Application.Features.Deducciones.Queries.GetAllDeducciones;
using Core.Application.Features.Percepciones.Commands.CreatePercepciones;
using Core.Application.Features.Percepciones.Queries.GetAllPercepcion;
using Core.Application.Interface.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var response = await _userService.GetAllClients();
            return View(response);
        }

        public async Task<IActionResult> Deducciones()
        {
            var response = await _mediator.Send(new GetAllDeduccionesQuery());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeducciones(CreateDeduccionesCommand comm)
        {
            await _mediator.Send(comm);
            return RedirectToAction("Deducciones");
        }

        public async Task<IActionResult> Percepciones()
        {
            var response = await _mediator.Send(new GetAllPercepcionQuery());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePercepciones(CreatePercepcionesCommand comm)
        {
            await _mediator.Send(comm);
            return RedirectToAction("Percepciones");
        }
    }
}
