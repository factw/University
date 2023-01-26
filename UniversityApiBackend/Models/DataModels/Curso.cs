using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Curso : BaseEntity
    {
        [StringLength(280)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(280)]
        public string DescripcionCorta { get; set; } = string.Empty;

        public string DescripcionLarga { get; set; } = string.Empty;

        public string PublicoObjetivo { get; set; } = string.Empty;
        
        public string Objetivo { get; set; } = string.Empty;

        public string Requisitos { get; set;} = string.Empty;

        public enum Nivel {Basico, Intermedio, Avanzado}
    }
}
