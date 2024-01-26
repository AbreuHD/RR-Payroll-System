using Azure.Core;
using Core.Application.DTOs.Toast;
using Core.Application.Features.Banco.Commands.CreateBanco;
using Core.Application.Features.Banco.Queries.GetAllTipoBanco;
using Core.Application.Features.Deducciones.Commands.CreateDeduccion;
using Core.Application.Features.Deducciones.Queries.GetAllDeducciones;
using Core.Application.Features.Estado.Command.CreateEstado;
using Core.Application.Features.Estado.Queries.GetAllEstado;
using Core.Application.Features.Nacionalidad.Commands.CreateNacionalidad;
using Core.Application.Features.Nacionalidad.Queries.GetAllNacionalidad;
using Core.Application.Features.Percepciones.Commands.CreatePercepciones;
using Core.Application.Features.Percepciones.Queries.GetAllPercepcion;
using Core.Application.Features.Provincia.Command.CreateProvincia;
using Core.Application.Features.Provincia.Queries.GetAllProvincia;
using Core.Application.Features.Puesto.Command.CreatePuesto;
using Core.Application.Features.Puesto.Queries.GetAllPuestos;
using Core.Application.Features.TipoCuenta.Commands.CreateTipoCuenta;
using Core.Application.Features.TipoPago.Commands.CreateTipoPago;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Controllers
{
    public class ExtraAdmin : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Puesto()
        {
            if(TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllPuestosQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CrearPuesto(CreatePuestoCommand request)
        {
            var toast = new ToastRequestDTO()
            {
                Title = "Puesto creado",
                Message = "El puesto ha sido creado correctamente",
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast);
            await Mediator.Send(request);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("Puesto");
        }

        public async Task<IActionResult> Estado()
        {
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllEstadoQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CrearEstado(CreateEstadoCommand request)
        {
            var toast = new ToastRequestDTO()
            {
                Title = "Estado creado",
                Message = "El estado ha sido creado correctamente",
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast); 
            await Mediator.Send(request);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("Estado");
        }

        public async Task<IActionResult> Nacionalidad()
        {
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllNacionalidadQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CrearNacionalidad(CreateNacionalidadCommand request)
        {
            var toast = new ToastRequestDTO()
            {
                Title = "Nacionalidad creada",
                Message = "La nacionalidad ha sido creada correctamente",
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast);
            await Mediator.Send(request);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("Nacionalidad");
        }

        public async Task<IActionResult> Provincia()
        {
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllProvinciaQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateProvincia(CreateProvinciaCommand request)
        {
            var toast = new ToastRequestDTO()
            {
                Title = "Provincia creada",
                Message = "La provincia ha sido creada correctamente",
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast);
            await Mediator.Send(request);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("Provincia");
        }

        public async Task<IActionResult> TipoCuenta()
        {
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllTTipoCuentaQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoCuenta(CreateTipoCuentaCommand request)
        {
            var toast = new ToastRequestDTO()
            {
                Title = "Tipo de cuenta creado",
                Message = "El tipo de cuenta ha sido creado correctamente",
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast);
            await Mediator.Send(request);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("TipoCuenta");
        }

        public async Task<IActionResult> TipoBanco()
        {
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllTipoBancoQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoBanco(CreateTipoBancoCommand request)
        {
            var toast = new ToastRequestDTO()
            {
                Title = "Tipo de banco creado",
                Message = "El tipo de banco ha sido creado correctamente",
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast);
            await Mediator.Send(request);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("TipoBanco");
        }

        public async Task<IActionResult> Deducciones()
        {
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllDeduccionesQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeducciones(CreateDeduccionesCommand comm)
        {
            var toast = new ToastRequestDTO() { 
                Title = "Deducción creada", 
                Message = "La deducción ha sido creada correctamente" 
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast);
            comm.IsDefault = comm.DefaultBool == 1;
            await Mediator.Send(comm);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("Deducciones");
        }

        public async Task<IActionResult> Percepciones()
        {
            if (TempData["Created"] != null)
            {
                ViewBag.created = JsonConvert.DeserializeObject<List<ToastRequestDTO>>((string)TempData["Created"]);
            }
            return View(await Mediator.Send(new GetAllPercepcionQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePercepciones(CreatePercepcionesCommand comm)
        {
            var toast = new ToastRequestDTO()
            {
                Title = "Percepción creada",
                Message = "La percepción ha sido creada correctamente"
            };
            List<ToastRequestDTO> toasts = new List<ToastRequestDTO>();
            toasts.Add(toast);
            comm.IsDefault = comm.DefaultBool == 1;
            await Mediator.Send(comm);
            TempData["Created"] = JsonConvert.SerializeObject(toasts);
            return RedirectToAction("Percepciones");
        }
    }
}
