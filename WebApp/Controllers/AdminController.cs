using Core.Application.Interface.Services;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
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
        public async Task<IActionResult> CreateUser(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            var data = await _userService.RegisterAdmin(vm);
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
        public async Task<IActionResult> ReportesProyecto()
        {
            return View();
        }
        public async Task<IActionResult> Proyectos()
        {
            return View();
        }
        public async Task<IActionResult> Deducciones()
        {
            return View();
        }
    }
}
