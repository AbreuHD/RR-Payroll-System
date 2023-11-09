using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
