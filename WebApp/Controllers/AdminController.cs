using Core.Application.Features.Empleado.Comands.CreateEmpleado;
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
            var data = await _userService.GetAllClients();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserSaveViewModel vm, CreateEmpleadoCommand comm)
        {
            try
            {
                comm.Telefono = vm.Phone;
                comm.Nombre = vm.Name;
                comm.Apellido = vm.LastName;
            }catch(Exception e)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToRoute(new { controller = "Admin", action = "Index" });
            //}
            var data = await _userService.RegisterAdmin(vm);
            comm.UserID = data.Id;
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
