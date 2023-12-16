using Core.Application.DTOs.Account;
using Core.Application.DTOs.Empleados;
using Core.Application.Features.Asistencia.Queries.GetAllAsistenciaByUserID;
using Core.Application.Features.Empleado.Comands.UpdateEmpleado;
using Core.Application.Features.Empleado.Queries.GetAllEmpleados;
using Core.Application.Features.Empleado.Queries.GetEmpleadoById;
using Core.Application.Features.EmpleadoProyecto.Queries.GetAllByEmployeeId;
using Core.Application.Features.EmpleadoProyecto.Queries.GetAllProjects;
using Core.Application.Features.Estado.Queries.GetAllEstado;
using Core.Application.Features.Horas.Command.AddHoras;
using Core.Application.Features.Nacionalidad.Queries.GetAllNacionalidad;
using Core.Application.Features.Permiso.Command.CrearPermiso;
using Core.Application.Features.Provincia.Queries.GetAllProvincia;
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

        public async Task<IActionResult> UpdateEmployee(int Id)
        {
            ViewBag.Estado = await Mediator.Send(new GetAllEstadoQuery());
            ViewBag.Provincia = await Mediator.Send(new GetAllProvinciaQuery());
            ViewBag.Nacionalidad = await Mediator.Send(new GetAllNacionalidadQuery());
            var response =  await Mediator.Send(new GetEmpleadoByIdQuery() { Id = Id } );
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployees(UpdateEmpleadoCommand comm)
        {
            await Mediator.Send(comm);
            return RedirectToAction("Admin");
        }

        public async Task<IActionResult> InfoPoncheEmployee(int Id)
        {

            var response = await Mediator.Send(new GetAllByEmployeeIdQuery() { IdUser = Id });
            return View(response);
        }

        public async Task<IActionResult> InfoPoncheHistorico(int Id)
        {
            var response = await Mediator.Send(new GetAllAsistenciaByUserIDQuery() { EmpleadoProyectoID = Id });
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPermiso(CrearPermisoCommand comm, int infoId)
        {
            await Mediator.Send(comm);
            return RedirectToAction($"InfoPoncheHistorico/{infoId}");
        }
    }
}
