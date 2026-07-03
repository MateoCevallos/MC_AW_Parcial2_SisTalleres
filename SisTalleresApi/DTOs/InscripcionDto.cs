namespace SisTalleresApi.DTOs
{
    public class InscripcionDto
    {
        public int InscripcionId { get; set; }
        public DateOnly FechaInscripcion { get; set; }
        public string Estado { get; set; } = string.Empty;

        public int TallerId { get; set; }
        public string TallerNombre { get; set; } = string.Empty;
        public DateOnly TallerFecha { get; set; }
        public string TallerUbicacion { get; set; } = string.Empty;

        public int ParticipanteId { get; set; }
        public string ParticipanteNombre { get; set; } = string.Empty;
        public string ParticipanteApellido { get; set; } = string.Empty;
        public string ParticipanteEmail { get; set; } = string.Empty;
    }
}