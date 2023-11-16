using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
