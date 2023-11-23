﻿using Azure.Core;
using Core.Application.Features.Estado.Command.CreateEstado;
using Core.Application.Features.Estado.Queries.GetAllEstado;
using Core.Application.Features.Nacionalidad.Commands.CreateNacionalidad;
using Core.Application.Features.Nacionalidad.Queries.GetAllNacionalidad;
using Core.Application.Features.Provincia.Command.CreateProvincia;
using Core.Application.Features.Provincia.Queries.GetAllProvincia;
using Core.Application.Features.Puesto.Command.CreatePuesto;
using Core.Application.Features.Puesto.Queries.GetAllPuestos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            return View(await Mediator.Send(new GetAllPuestosQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CrearPuesto(CreatePuestoCommand request)
        {
            await Mediator.Send(request);
            return RedirectToAction("Puesto");
        }

        public async Task<IActionResult> Estado()
        {
            return View(await Mediator.Send(new GetAllEstadoQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CrearEstado(CreateEstadoCommand request)
        {
            await Mediator.Send(request);
            return RedirectToAction("Estado");
        }

        public async Task<IActionResult> Nacionalidad()
        {
            return View(await Mediator.Send(new GetAllNacionalidadQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CrearNacionalidad(CreateNacionalidadCommand request)
        {
            await Mediator.Send(request);
            return RedirectToAction("Nacionalidad");
        }

        public async Task<IActionResult> Provincia()
        {
            return View(await Mediator.Send(new GetAllProvinciaQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateProvincia(CreateProvinciaCommand request)
        {
            await Mediator.Send(request);
            return RedirectToAction("Provincia");

        }
    }
}