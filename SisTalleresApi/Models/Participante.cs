using System.ComponentModel.DataAnnotations;

namespace SisTalleresApi.Models
{
    public class Participante
    {
        [Key]
        public int ParticipanteId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telefono { get; set; }

        // Relación: un participante tiene muchas inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}