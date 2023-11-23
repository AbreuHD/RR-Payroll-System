﻿using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.DTOs.Empleados;
using Core.Application.DTOs.Estado;
using Core.Application.DTOs.Proyecto;
using Core.Application.DTOs.Puestos;
using Core.Application.Features.Empleado.Comands.CreateEmpleado;
using Core.Application.Features.EmpleadoProyecto.Commands.CreateEmpleadoProyecto;
using Core.Application.Features.Proyecto.Commands.CreateProject;
using Core.Application.Features.Puesto.Command.CreatePuesto;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
            CreateMap<RegisterRequest, UserSaveViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap()
                        .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive));
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.Error, opt => opt.Ignore())
                        .ReverseMap();

            CreateMap<GetAllStatusDTO, Estado>()
                .ReverseMap();

            CreateMap<CreateProjectCommand, Proyecto>()
                .ReverseMap();

            CreateMap<ProyectoDTO, Proyecto>()
                .ReverseMap();

            CreateMap<CreatePuestoCommand, Puesto>()
                .ReverseMap();

            CreateMap<CreateEmpleadoCommand, Empleado>()
                .ReverseMap();

            CreateMap<CreateEmpleadoProyectoCommand, EmpleadoProyecto>()
                .ReverseMap();

            CreateMap<GetAllEmpleadosResponseDTO, Empleado>()
                .ReverseMap();

            CreateMap<GetAllPuestosResponseDTO, Puesto>()
                .ReverseMap();
        }
    }
}
