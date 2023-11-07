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
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateSession _validateSession;


        public LoginController(IUserService userService, ValidateSession validateSession)
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
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            AuthenticationResponse user = await _userService.Login(loginViewModel);
            if (user.HasError || user == null)
            {
                loginViewModel.HasError = user.HasError;
                loginViewModel.Error = user.Error;
                ModelState.AddModelError("userValidation", "Datos de acceso incorrectos");
                return View(loginViewModel);
            }
            //here is where the session is set
            var jsonSerializerSettings = new JsonSerializerSettings{};
            var userJson = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var userBytes = Encoding.UTF8.GetBytes(userJson);
            HttpContext.Session.Set("user", userBytes);

            if (user.Roles.Any(rol => rol == "Admin"))
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
        public async Task<IActionResult> LogOut()
        {
            await _userService.SingOut();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmail(userId, token);
            return View("ConfirmEmail", response);
        }

        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse forgotResponse = await _userService.ForgotPassword(forgotPassword, origin);
            if (forgotResponse.HasError)
            {
                forgotPassword.HasError = forgotResponse.HasError;
                forgotPassword.Error = forgotResponse.Error;

                return View(forgotPassword);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }
            ResetPasswordResponse resetResponse = await _userService.ResetPassword(resetPassword);
            if (resetResponse.HasError)
            {
                resetPassword.HasError = resetResponse.HasError;
                resetPassword.Error = resetResponse.Error;

                return View(resetPassword);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
