using Core.Application.DTOs.Account;
using Core.Application.Interface.Services;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Middleware;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateSession _validateSession;


        public HomeController(IUserService userService, ValidateSession validateSession)
        {
            _userService = userService;
            _validateSession = validateSession;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            AuthenticationResponse user = await _userService.Login(loginViewModel);
            if (user.HasError || user == null)
            {
                loginViewModel.HasError = user.HasError;
                loginViewModel.Error = user.Error;
                ModelState.AddModelError("userValidation", "Datos de acceso incorrectos");
                return View(loginViewModel);
            }
            var jsonSerializerSettings = new JsonSerializerSettings{};
            var userJson = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var userBytes = Encoding.UTF8.GetBytes(userJson);
            HttpContext.Session.Set("user", userBytes);

            if(!user.isTheFirsTime)
            {
                TempData["user"] = user.UserName;
                TempData["userId"] = user.Id;
                return RedirectToRoute(new { controller = "Home", action = "FirstLogin" });
            }

            if (user.Roles.Any(rol => rol == "Admin"))
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            return RedirectToRoute(new { controller = "Employee", action = "Index" });
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SingOut();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> FirstLogin()
        {
            if(TempData["user"] == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            ViewBag.Regex = " /^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?\":{}|<>])(.{8,})$/";
            ViewBag.user = TempData["user"];
            ViewBag.userId = TempData["userId"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ResetPasswordViewModel vm)
        {
            await _userService.ResetPassword(vm);

            return RedirectToAction("LogOut");
        }

    }
}
