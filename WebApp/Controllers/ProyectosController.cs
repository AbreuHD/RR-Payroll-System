using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ProyectosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(int id)
        {
            return View();
        }

    }
}
