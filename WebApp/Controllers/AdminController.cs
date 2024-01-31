using Core.Application.DTOs.Register;
using Core.Application.DTOs.Toast;
using Core.Application.Features.Banco.Queries.GetAllTipoBanco;
using Core.Application.Features.Empleado.Comands.CreateEmpleado;
using Core.Application.Features.Empleado.Comands.UpdateEmpleado;
using Core.Application.Features.Empleado.Queries.GetEmpleadoByIdentityId;
using Core.Application.Features.Estado.Queries.GetAllEstado;
using Core.Application.Features.Nacionalidad.Queries.GetAllNacionalidad;
using Core.Application.Features.Provincia.Queries.GetAllProvincia;
using Core.Application.Features.TipoPago.Commands.CreateTipoPago;
using Core.Application.Features.TipoPago.Commands.UpdateTipoPago;
using Core.Application.Features.TipoPago.Queries.GetTipoPagoByIdentityId;
using Core.Application.Interface.Services;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            ViewBag.TipoCuenta = await Mediator.Send(new GetAllTTipoCuentaQuery());
            ViewBag.TipoBanco = await Mediator.Send(new GetAllTipoBancoQuery());

            var data = await _userService.GetAllClients();
            //foreach (var item in data)
            //{
            //    var result = await Mediator.Send(new GetEmpleadoByIdentityIdQuery { IdentityId = item.Id });
            //    //item.EsEmpleado = result;
            //}
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmpleado(CreateEmpleadoCommand comm, string IdentityId, CreateTipoPagoCommand commPago)
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
            commPago.IdUsuario = user.Id;
            await Mediator.Send(commPago); //pago
            comm.UserID = user.Id;
            await Mediator.Send(comm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> User(string? id)
        {
            ViewBag.Nacionalidades = await Mediator.Send(new GetAllNacionalidadQuery());
            ViewBag.Provincias = await Mediator.Send(new GetAllProvinciaQuery());
            ViewBag.Estados = await Mediator.Send(new GetAllEstadoQuery());

            ViewBag.TipoCuenta = await Mediator.Send(new GetAllTTipoCuentaQuery());
            ViewBag.TipoBanco = await Mediator.Send(new GetAllTipoBancoQuery());
            if(id != null)
            {
                var request = new RegisterRequestDTO();

                var accountData = await _userService.GetAccountByid(id);
                request = await Mediator.Send(new GetEmpleadoByIdentityIdQuery { IdentityId = id, Data = request });
                request = await Mediator.Send(new GetTipoPagoByIdentityIdQuery { IdentityId = id, Data = request });
                request.Id = id;
                return View(request);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserMod(RegisterRequestDTO dTO, bool isEditMode)
        {
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();

            if (isEditMode)
            {
                var sucess = await _userService.Update(dTO);
                if(sucess.HasError)
                {
                    TempData["Error"] = JsonConvert.SerializeObject(sucess.Toasts);
                    return RedirectToAction("Index");
                }
                else
                {
                    UpdateEmpleadoCommand comm = new UpdateEmpleadoCommand()
                    {
                        EmpleadoId = dTO.IdEmpleado,
                        Apellido = dTO.Apellido,
                        Nombre = dTO.Nombre,
                        Celular = dTO.Telefono,
                        Direccion = dTO.Direccion,
                        Email = dTO.Correo,
                        FechaNacimiento = dTO.Nacimiento,
                        IdEstado = dTO.Estado,
                        IdNacionalidad = dTO.Nacionalidad,
                        IdProvincia = dTO.Provincia,
                        UserID = dTO.Id,
                        Telefono = dTO.Telefono,
                        Sexo = (dTO.Genero == 1) ? true : false,
                        Cedula = dTO.Cedula,
                    };

                    UpdateTipoPagoCommand commPago = new UpdateTipoPagoCommand()
                    {
                        IdTipoPago = dTO.IdTipoPago,
                        Cuenta = dTO.Cuenta,
                        IdTipoCuenta = dTO.TipoCuenta,
                        IdTipoBanco = dTO.Banco,
                        IdUsuario = dTO.Id,
                    };
                    await Mediator.Send(comm);
                    await Mediator.Send(commPago);

                    var toast = new ToastRequestDTO();
                    toast.Title = "Usuario Actualizado";
                    toast.Message = "El usuario se ha actualizado";
                    toasts.Add(toast);
                    TempData["Updated"] = JsonConvert.SerializeObject(toasts);
                }   
            }
            else
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

                var toast  = new ToastRequestDTO();
                toast.Title = "Usuario creado";
                toast.Message = "El usuario se ha creado correctamente";
                toasts.Add(toast);
                TempData["Created"] = JsonConvert.SerializeObject(toasts);

            }
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        //[HttpPost]
        //public async Task<IActionResult> UserEditar(UserSaveViewModel vm)
        //{
        //    //var data = await _userService.UpdateClient(vm);
        //    return RedirectToRoute(new { controller = "Admin", action = "Index" });
        //}

        public async Task<IActionResult> Desactivar(string UserId, int TipoUsuario) 
        {
            await _userService.ChangeStatus(UserId, TipoUsuario);
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
