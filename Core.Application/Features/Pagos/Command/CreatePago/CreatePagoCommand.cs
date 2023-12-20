using AutoMapper;
using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Pagos.Command.CreatePago
{
    public class CreatePagoCommand : IRequest<bool>
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Comision { get; set; }
        //public string Emisor { get; set; }
        public int IdTipoPago { get; set; }
        public int IdEmpleado { get; set; }
        public List<int> IdPago_Percepciones { get; set; }
        public List<int> IdPago_Deducciones { get; set; }

        public int IdProyecto { get; set; }
    }
    public class CreatePagoCommandHandler : IRequestHandler<CreatePagoCommand, bool>
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IDeduccionesRepository _deduccionesRepository;
        private readonly IPercepcionesRepository _percepcionesRepository;
        private readonly ITipoPagoRepository _tipoPagoRepository;
        private readonly IMapper _mapper;
        private readonly IPago_DeduccionesRepository _pago_DeduccionesRepository;
        private readonly IPago_PercepcionesRepository _pago_PercepcionesRepository;
        private readonly IEmpleadoProyectosRepository _empleadoProyectosRepository;
        private readonly IHorasRepository _horasRepository;
        private readonly IActividadesAsignadasRepository _actividadesAsignadasRepository;

        public CreatePagoCommandHandler(IPagoRepository pagoRepository, IMapper mapper, IDeduccionesRepository deduccionesRepository, IPercepcionesRepository percepcionesRepository, ITipoPagoRepository tipoPagoRepository, IPago_DeduccionesRepository pago_DeduccionesRepository, IPago_PercepcionesRepository pago_PercepcionesRepository, IEmpleadoProyectosRepository empleadoProyectosRepository, IHorasRepository horasRepository, IActividadesAsignadasRepository actividadesAsignadasRepository)
        {
            _pagoRepository = pagoRepository;
            _mapper = mapper;
            _deduccionesRepository = deduccionesRepository;
            _percepcionesRepository = percepcionesRepository;
            _tipoPagoRepository = tipoPagoRepository;
            _pago_DeduccionesRepository = pago_DeduccionesRepository;
            _pago_PercepcionesRepository = pago_PercepcionesRepository;
            _empleadoProyectosRepository = empleadoProyectosRepository;
            _horasRepository = horasRepository;
            _actividadesAsignadasRepository = actividadesAsignadasRepository;
        }
        public async Task<bool> Handle(CreatePagoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deducciones = await _deduccionesRepository.GetAllAsync();
                var percepciones = await _percepcionesRepository.GetAllAsync();
                var empleadoProyecto = await _empleadoProyectosRepository.GetAllWithIncludes(new List<string> { "Asistencia", "ActividadesAsignadas", "Empleado" });
                var tipoPago = await _tipoPagoRepository.GetAllAsync();

                var empleadoSeleccionado = empleadoProyecto.FirstOrDefault(x => x.IdEmpleado == request.IdEmpleado && x.IdProyecto == request.IdProyecto);
                var percecionesDefault = percepciones.Where(x => x.IsDefault == true).ToList();
                var deduccionesDefault = deducciones.Where(x => x.IsDefault == true).ToList();
                var tipoPagoSel = tipoPago.Where(x => x.IdUsuario == empleadoSeleccionado.Empleado.UserID).FirstOrDefault();               

                var requestPago = _mapper.Map<Pago>(request);
                requestPago.IdProyecto = request.IdProyecto;
                requestPago.IdTipoPago = tipoPagoSel.Id;
                var response = await _pagoRepository.AddAsync(requestPago);

                decimal sumaPercepcion = 0.0m;
                decimal sumaDeduccion = 0.0m;

                foreach (var item in request.IdPago_Deducciones)
                {
                    var pago_Deducciones = new Pago_Deducciones();
                    pago_Deducciones.IdDeducciones = item;
                    pago_Deducciones.IdPago = response.Id;
                    await _pago_DeduccionesRepository.AddAsync(pago_Deducciones);
                    sumaPercepcion += deducciones.FirstOrDefault(x => x.Id == item).Monto;
                }
                foreach(var item in deduccionesDefault) {
                    var pago_Deducciones = new Pago_Deducciones();
                    pago_Deducciones.IdDeducciones = item.Id;
                    pago_Deducciones.IdPago = response.Id;
                    await _pago_DeduccionesRepository.AddAsync(pago_Deducciones);
                    sumaPercepcion += item.Monto;
                }

                foreach (var item in request.IdPago_Percepciones)
                {
                    var pago_Percepciones = new Pago_Percepciones();
                    pago_Percepciones.IdPercepciones = item;
                    pago_Percepciones.IdPago = response.Id;
                    await _pago_PercepcionesRepository.AddAsync(pago_Percepciones);
                    sumaDeduccion += percepciones.FirstOrDefault(x => x.Id == item).Monto;
                }
                foreach (var item in percecionesDefault)
                {
                    var pago_Percepciones = new Pago_Percepciones();
                    pago_Percepciones.IdPercepciones = item.Id;
                    pago_Percepciones.IdPago = response.Id;
                    await _pago_PercepcionesRepository.AddAsync(pago_Percepciones);
                    sumaDeduccion += item.Monto;
                }

                var horas = await _horasRepository.GetAllAsync();
                var horasEmpleado = horas.Where(x => x.IdAsistencia == empleadoSeleccionado.Asistencia.Id).ToList();
                
                var actividadesAsignadas = await _actividadesAsignadasRepository.GetAllWithIncludes(new List<string> { "Actividad" });
                var actividadesAsignadasEmpleado = actividadesAsignadas.Where(x => x.IdEmpleadoProyecto == empleadoSeleccionado.Id).ToList();

                decimal suma = 0.0m;

                foreach (var i in actividadesAsignadasEmpleado)
                {
                    suma = + i.Actividad.Monto;
                }

                foreach (var i in horasEmpleado)
                {
                    if(i.HoraFinal != null)
                    {
                        TimeSpan sumaHoras = (TimeSpan)(i.HoraFinal - i.HorasInicio);
                        if (sumaHoras.Hours > empleadoSeleccionado.Horas)
                        {
                            var extra = sumaHoras.Hours - empleadoSeleccionado.Horas;
                            suma += (decimal)(+ (extra * empleadoSeleccionado.PagoHoras) * 0.20);
                            suma += (decimal)((sumaHoras.Hours - extra) * empleadoSeleccionado.PagoHoras);
                        }
                    }
                }
                response.Monto = (suma + sumaPercepcion) - sumaDeduccion;
                response.Comision = sumaDeduccion;
                response.Fecha = DateTime.Now;
                await _pagoRepository.UpdateAsync(response, response.Id);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }   
}
