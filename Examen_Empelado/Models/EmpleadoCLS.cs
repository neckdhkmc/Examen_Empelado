using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Examen_Empelado.Models
{
    public class EmpleadoCLS
    {


        public int id { get; set; }

        public string Nombre { get; set; }
        [Display(Name = "Apellido Paterno")]

        [Required]
        public string ApellidoPaterno { get; set; }
        [Display(Name = "Apellido Materno")]
        [Required]
        public string ApellidoMaterno { get; set; }
        [Display(Name = "Numero de empleado")]

        public string NumEmpleado { get; set; }
        [Display(Name = "Correo")]

        public string correo { get; set; }
        [Display(Name = "Telefono Movil")]

        public string Telefono { get; set; }
        public string Direccion { get; set; }
        [Display(Name = "Fecha de Nacimiento")]

        public string FechaNac { get; set; }

        [Display(Name = "Estado del empleado")]

        public int idEstadoLogico { get; set; }

        //propiedades adicionales


        [Display(Name = "Estado Logico")]
        public string NombreEstadoLogico { get; set; }

        public string mensajeError { get; set; }

    }
}