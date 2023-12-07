using Core.Application.Features.Empleado.Comands.CreateEmpleado;
using Core.Application.Features.Empleado.Queries.GetEmpleadoByIdentityId;
using Core.Application.Features.Estado.Queries.GetAllEstado;
using Core.Application.Features.Nacionalidad.Queries.GetAllNacionalidad;
using Core.Application.Features.Provincia.Queries.GetAllProvincia;
using Core.Application.Interface.Services;
using Core.Application.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Nacionalidades = await Mediator.Send(new GetAllNacionalidadQuery());
            ViewBag.Provincias = await Mediator.Send(new GetAllProvinciaQuery());
            ViewBag.Estados = await Mediator.Send(new GetAllEstadoQuery());
            var data = await _userService.GetAllClients();
            foreach (var item in data)
            {
                var result = await Mediator.Send(new GetEmpleadoByIdentityIdQuery { IdentityId = item.Id });
                item.EsEmpleado = result;
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserSaveViewModel vm)
        {
            var data = await _userService.RegisterAdmin(vm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpleado(CreateEmpleadoCommand comm, string IdentityId)
        {
            var user = await _userService.GetAccountByid(IdentityId);
            try
            {
                comm.Telefono = user.Phone;
                comm.Nombre = user.Name;
                comm.Apellido = user.LastName;
                comm.Email = user.Email;
                comm.Telefono = user.Phone;
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            comm.UserID = user.Id;
            await Mediator.Send(comm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> EditarUsuario(string id)
        {
            var data = await _userService.GetAccountByid(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UserEditar(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            var data = await _userService.UpdateClient(vm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> Desactivar(string id)
        {
            await _userService.ChangeStatus(id);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> Asistencia()
        {
            return View();
        }
        public async Task<IActionResult> Pagos()
        {
            return View();
        }
        public async Task<IActionResult> Deducciones()
        {
            return View();
        }
    }
}
