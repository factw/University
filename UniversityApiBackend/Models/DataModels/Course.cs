using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        
        [Required]
        public string Description{ get; set; } = string.Empty;

        public Level Level { get; set; } = Level.Basic;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();
    }

    public enum Level 
    { 
        Basic,
        Medium, 
        Advanced,
        Expert
    }
}
