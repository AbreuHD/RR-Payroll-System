using Core.Application.DTOs.Account;
using Core.Application.Features.Empleado.Queries.GetAllEmpleados;
using Core.Application.Features.EmpleadoProyecto.Queries.GetAllProjects;
using Core.Application.Features.Horas.Command.AddHoras;
using Core.Application.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Middleware;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly IHttpContextAccessor _httpContext;
        public EmployeeController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = _httpContext.HttpContext.Session.Id != null ? JsonConvert.DeserializeObject<UserViewModel>(_httpContext.HttpContext.Session.GetString("user")) : null;
            var response = await Mediator.Send(new GetAllProjectsQuery() { IdUser = user.Id });
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Ponche(int id)
        {
            await Mediator.Send(new AddHorasCommand() { IdEmpleadoProyecto = id });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Admin()
        {
            var response = await Mediator.Send(new GetAllEmpleadosSinProyecto());
            
            return View(response);
        }

        public async Task<IActionResult> Info(string Id)
        {
            var response = await Mediator.Send(new GetAllProjectsQuery() { IdUser = Id });
            return View(response);
        }
    }
}
