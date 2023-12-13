using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.DTOs.Actividades;
using Core.Application.DTOs.Banco;
using Core.Application.DTOs.EmpleadoProyecto;
using Core.Application.DTOs.Empleados;
using Core.Application.DTOs.Estado;
using Core.Application.DTOs.Nacionalidad;
using Core.Application.DTOs.Provincia;
using Core.Application.DTOs.Proyecto;
using Core.Application.DTOs.Puestos;
using Core.Application.DTOs.TipoCuenta;
using Core.Application.Features.Actividades.Command.CreateActividades;
using Core.Application.Features.ActividadesAsignadas.Commands.CreateActAsignada;
using Core.Application.Features.Asistencia.Command.CreateAsistenciaTable;
using Core.Application.Features.Deducciones.Commands.CreateDeduccion;
using Core.Application.Features.Empleado.Comands.CreateEmpleado;
using Core.Application.Features.EmpleadoProyecto.Commands.CreateEmpleadoProyecto;
using Core.Application.Features.EmpleadoProyecto.Commands.EditEmpleadoProyecto;
using Core.Application.Features.Estado.Command.CreateEstado;
using Core.Application.Features.Horas.Command.AddHoras;
using Core.Application.Features.Nacionalidad.Commands.CreateNacionalidad;
using Core.Application.Features.Percepciones.Commands.CreatePercepciones;
using Core.Application.Features.Provincia.Command.CreateProvincia;
using Core.Application.Features.Proyecto.Commands.CreateProject;
using Core.Application.Features.Proyecto.Commands.EditProject;
using Core.Application.Features.Puesto.Command.CreatePuesto;
using Core.Application.Features.TipoPago.Commands.CreateTipoPago;
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

            CreateMap<CreateEmpleadoProyectoCommand, EmpleadoProyectos>()
                .ReverseMap();

            CreateMap<GetAllEmpleadosResponseDTO, Empleado>()
                .ReverseMap();

            CreateMap<GetAllPuestosResponseDTO, Puesto>()
                .ReverseMap();

            CreateMap<CreateEstadoCommand, Estado>()
                .ReverseMap();

            CreateMap<CreateNacionalidadCommand, Nacionalidad>()
                .ReverseMap();

            CreateMap<CreateProvinciaCommand, Provincia>()
                .ReverseMap();

            CreateMap<GetAllPuestosResponseDTO, Puesto>()
                .ReverseMap();

            CreateMap<CreateEstadoCommand, Estado>()
                .ReverseMap();

            CreateMap<GetAllNacionalidadResponseDTO, Nacionalidad>()
                .ReverseMap();

            CreateMap<ProvinciaResponseDTO, Provincia>()
                .ReverseMap();

            CreateMap<EmpleadoProyectos, EditEmpleadoProyectoCommand>()
                .ReverseMap();
            
            CreateMap<EditProjectCommand, Proyecto>()
                .ReverseMap();

            CreateMap<CreateAsistenciaTableCommand, Asistencia>()
                .ReverseMap();

            CreateMap<Horas, AddHorasCommand>()
                .ReverseMap();

            CreateMap<EmpleadoProyectos, EmpleadoProyectoReponseDTO>()
                .ReverseMap();

            CreateMap<TipoBanco, TipoBancoResponseDTO>()
                .ReverseMap();
            
            CreateMap<TipoCuenta, TipoCuentaResponseDTO>()
                .ReverseMap();
            
            CreateMap<Deducciones, CreateDeduccionesCommand>()
                .ReverseMap();
                        
            CreateMap<Percepciones, CreatePercepcionesCommand>()
                .ReverseMap();
                        
            CreateMap<TipoPago, CreateTipoPagoCommand>()
                .ReverseMap();

            CreateMap<Actividades, CreateActividadesCommand>()
                .ReverseMap();

            CreateMap<Actividades, GetAllActividadesResponseDTO>()
                .ReverseMap();            
            
            CreateMap<Actividades, GetActivityByIdResponseDTO>()
                .ReverseMap();            
            
            CreateMap<Empleado, GetAllEmpleadosResponseDTO>()
                .ForMember(x=> x.EmpleadoProyectos, i => i.Ignore())
                .ReverseMap();            
            
            
            CreateMap<ActividadesAsignadas, CreateActAsignadaCommand>()
                .ReverseMap();
        }
    }
}
