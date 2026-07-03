using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisTalleresApi.Models
{
    public enum EstadoInscripcion
    {
        confirmada,
        cancelada
    }

    public class Inscripcion
    {
        [Key]
        public int InscripcionId { get; set; }

        [Required]
        public int TallerId { get; set; }

        [ForeignKey("TallerId")]
        public Taller? Taller { get; set; }

        [Required]
        public int ParticipanteId { get; set; }

        [ForeignKey("ParticipanteId")]
        public Participante? Participante { get; set; }

        public DateOnly FechaInscripcion { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public EstadoInscripcion Estado { get; set; } = EstadoInscripcion.confirmada;
    }
}