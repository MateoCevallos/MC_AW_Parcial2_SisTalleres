using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisTalleresApi.Data;
using SisTalleresApi.Models;
using SisTalleresApi.DTOs;

namespace SisTalleresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly SisTalleresContext _context;

        public InscripcionesController(SisTalleresContext context)
        {
            _context = context;
        }

        // GET: api/inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionDto>>> GetInscripciones()
        {
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Taller)
                .Include(i => i.Participante)
                .Select(i => new InscripcionDto
                {
                    InscripcionId = i.InscripcionId,
                    FechaInscripcion = i.FechaInscripcion,
                    Estado = i.Estado.ToString(),
                    TallerId = i.TallerId,
                    TallerNombre = i.Taller!.Nombre,
                    TallerFecha = i.Taller.Fecha,
                    TallerUbicacion = i.Taller.Ubicacion,
                    ParticipanteId = i.ParticipanteId,
                    ParticipanteNombre = i.Participante!.Nombre,
                    ParticipanteApellido = i.Participante.Apellido,
                    ParticipanteEmail = i.Participante.Email
                })
                .ToListAsync();

            return inscripciones;
        }

        // GET: api/inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionDto>> GetInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones
                .Include(i => i.Taller)
                .Include(i => i.Participante)
                .Where(i => i.InscripcionId == id)
                .Select(i => new InscripcionDto
                {
                    InscripcionId = i.InscripcionId,
                    FechaInscripcion = i.FechaInscripcion,
                    Estado = i.Estado.ToString(),
                    TallerId = i.TallerId,
                    TallerNombre = i.Taller!.Nombre,
                    TallerFecha = i.Taller.Fecha,
                    TallerUbicacion = i.Taller.Ubicacion,
                    ParticipanteId = i.ParticipanteId,
                    ParticipanteNombre = i.Participante!.Nombre,
                    ParticipanteApellido = i.Participante.Apellido,
                    ParticipanteEmail = i.Participante.Email
                })
                .FirstOrDefaultAsync();

            if (inscripcion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la inscripción con id {id}" });
            }

            return inscripcion;
        }

        // POST: api/inscripciones
        [HttpPost]
        public async Task<ActionResult<Inscripcion>> PostInscripcion(Inscripcion inscripcion)
        {
            _context.Inscripciones.Add(inscripcion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new { mensaje = "Este participante ya está inscrito en este taller" });
            }

            return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcion.InscripcionId }, inscripcion);
        }

        // PUT: api/inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion(int id, Inscripcion inscripcion)
        {
            if (id != inscripcion.InscripcionId)
            {
                return BadRequest(new { mensaje = "El id de la URL no coincide con el id de la inscripción enviada" });
            }

            _context.Entry(inscripcion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionExiste(id))
                {
                    return NotFound(new { mensaje = $"No se encontró la inscripción con id {id}" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
            {
                return NotFound(new { mensaje = $"No se encontró la inscripción con id {id}" });
            }

            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscripcionExiste(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }
    }
}