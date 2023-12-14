using Core.Application.Features.Horas.Command.AddHoras;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AsistenciaController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPonche()
        {
            await Mediator.Send(new AddHorasCommand() { IdEmpleadoProyecto = 1 });
            return RedirectToAction("Index");
        }
    }
}
