using Core.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Middleware;

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
            return View();
        }
    }
}
