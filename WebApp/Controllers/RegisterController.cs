using Core.Application.DTOs.Register;
using Core.Application.DTOs.Toast;
using Core.Application.Features.Banco.Queries.GetAllTipoBanco;
using Core.Application.Features.Empleado.Comands.CreateEmpleado;
using Core.Application.Features.Estado.Queries.GetAllEstado;
using Core.Application.Features.Nacionalidad.Queries.GetAllNacionalidad;
using Core.Application.Features.Provincia.Queries.GetAllProvincia;
using Core.Application.Features.TipoPago.Commands.CreateTipoPago;
using Core.Application.Interface.Services;
using Core.Application.ViewModels.User;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Nacionalidades = await Mediator.Send(new GetAllNacionalidadQuery());
            ViewBag.Provincias = await Mediator.Send(new GetAllProvinciaQuery());
            ViewBag.Estados = await Mediator.Send(new GetAllEstadoQuery());

            ViewBag.TipoCuenta = await Mediator.Send(new GetAllTTipoCuentaQuery());
            ViewBag.TipoBanco = await Mediator.Send(new GetAllTipoBancoQuery());
            if (TempData["Error"] != null)
            {
                ViewBag.Error = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Error"]);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCreate(RegisterRequestDTO dTO)
        {
            var data = await _userService.Register(dTO);
            if (data.Toasts.Count != 0)
            {
                TempData["Error"] = JsonConvert.SerializeObject(data.Toasts);
                return RedirectToAction("Index");
            }

            CreateEmpleadoCommand comm = new CreateEmpleadoCommand()
            {
                Apellido = dTO.Apellido,
                Nombre = dTO.Nombre,
                Celular = dTO.Telefono,
                Direccion = dTO.Direccion,
                Email = dTO.Correo,
                FechaNacimiento = dTO.Nacimiento,
                IdEstado = dTO.Estado,
                IdNacionalidad = dTO.Nacionalidad,
                IdProvincia = dTO.Provincia,
                UserID = data.Id,
                Telefono = dTO.Telefono,
                Sexo = (dTO.Genero == 1) ? true : false,
                Cedula = dTO.Cedula,
            };
            CreateTipoPagoCommand commPago = new CreateTipoPagoCommand()
            {
                Cuenta = dTO.Cuenta,
                IdTipoCuenta = dTO.TipoCuenta,
                IdTipoBanco = dTO.Banco,
                IdUsuario = data.Id,
            };
            var userData = await Mediator.Send(comm);

            await Mediator.Send(commPago);
            TempData["FirstLogin"] = data.Username;
            return RedirectToAction("Info");
        }

        public async Task<IActionResult> Info()
        {
            ViewBag.FirstLogin = TempData["FirstLogin"];
            return View();
        }
    }
}
