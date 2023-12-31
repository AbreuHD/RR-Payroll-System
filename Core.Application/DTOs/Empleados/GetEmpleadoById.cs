﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Empleados
{
    public class GetEmpleadoByID
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Sexo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int IdNacionalidad { get; set; }
        public int IdProvincia { get; set; }
        public int IdEstado { get; set; }
    }
}
