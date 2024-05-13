using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE
    {
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Sexo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        [Required]
        [Display(Name="Ciudad")]
        public int CodCiudad { get; set; }
        public string NombreCiudad { get; set; }
    }

    [MetadataType(typeof(AlumnoCE))]
    public partial class Alumno
    {
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }
        public string NombreCiudad { get; set; }
    }
}