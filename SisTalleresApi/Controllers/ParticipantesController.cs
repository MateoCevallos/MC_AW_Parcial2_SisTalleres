using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisTalleresApi.Data;
using SisTalleresApi.Models;

namespace SisTalleresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly SisTalleresContext _context;

        public ParticipantesController(SisTalleresContext context)
        {
            _context = context;
        }

        // GET: api/participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            return await _context.Participantes.ToListAsync();
        }

        // GET: api/participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);

            if (participante == null)
            {
                return NotFound(new { mensaje = $"No se encontró el participante con id {id}" });
            }

            return participante;
        }

        // POST: api/participantes
        [HttpPost]
        public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
        {
            _context.Participantes.Add(participante);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new { mensaje = "Ya existe un participante registrado con ese email" });
            }

            return CreatedAtAction(nameof(GetParticipante), new { id = participante.ParticipanteId }, participante);
        }

        // PUT: api/participantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipante(int id, Participante participante)
        {
            if (id != participante.ParticipanteId)
            {
                return BadRequest(new { mensaje = "El id de la URL no coincide con el id del participante enviado" });
            }

            _context.Entry(participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExiste(id))
                {
                    return NotFound(new { mensaje = $"No se encontró el participante con id {id}" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound(new { mensaje = $"No se encontró el participante con id {id}" });
            }

            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipanteExiste(int id)
        {
            return _context.Participantes.Any(e => e.ParticipanteId == id);
        }
    }
}