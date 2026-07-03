using System.ComponentModel.DataAnnotations;

namespace SisTalleresApi.Models
{
    public class Taller
    {
        [Key]
        public int TallerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required]
        public DateOnly Fecha { get; set; }

        [Required]
        [MaxLength(150)]
        public string Ubicacion { get; set; } = string.Empty;

        // Relación: un taller tiene muchas inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}