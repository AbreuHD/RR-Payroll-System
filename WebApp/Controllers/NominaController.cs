using ClosedXML.Excel;
using Core.Application.DTOs.Nomina;
using Core.Application.Features.Nomina.Queries.GetProjectNomina;
using Core.Application.Features.Proyecto.Queries.GetAllProjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebApp.Controllers
{
    public class NominaController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IActionResult> Index()
        {
            return View(await Mediator.Send(new GetAllProjectsQuery()));
        }

        [HttpGet]
        public async Task<IActionResult> Descargar(int ProjectId)
        {
            var response = await Mediator.Send(new GetProjectNominaQuery()
            {
                ProjectId = ProjectId
            });
            var nombre = "Nomina"+ DateOnly.FromDateTime(DateTime.Now).ToString();
            return DescargarExcel(nombre, response);
        }

        private FileResult DescargarExcel(string nombre, IEnumerable<NominaDTO> nomina)
        {
            DataTable dt = new DataTable("Nomina");
            dt.Columns.AddRange(new DataColumn[12]
            {
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Apellido", typeof(string)),
                new DataColumn("FechaNacimiento", typeof(DateTime)),
                new DataColumn("Fecha", typeof(DateTime)),
                new DataColumn("Monto", typeof(decimal)),
                new DataColumn("Comision", typeof(decimal)),
                new DataColumn("Deduccion", typeof(decimal)),
                new DataColumn("DeduccionDescripcion", typeof(string)),
                new DataColumn("Percepcion", typeof(decimal)),
                new DataColumn("PercepcionDescripcion", typeof(string)),
                new DataColumn("NombreProyecto", typeof(string)),
                new DataColumn("Neto", typeof(decimal)),
            });
            foreach (var item in nomina)
            {
                dt.Rows.Add(item.Nombre, item.Apellido, item.FechaNacimiento, item.Fecha.ToString(), item.Monto, item.Comision, item.Deduccion, item.DeduccionDescripcion, item.Percepcion, item.PercepcionDescripcion, item.NombreProyecto, item.Neto);
            }


            using (var package = new XLWorkbook())
            {
                package.AddWorksheet(dt);
                using(MemoryStream memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre + ".xlsx");
                }
            }

        }
    }
}
