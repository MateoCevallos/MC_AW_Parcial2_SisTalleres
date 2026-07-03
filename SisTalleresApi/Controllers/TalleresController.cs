using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisTalleresApi.Data;
using SisTalleresApi.Models;

namespace SisTalleresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalleresController : ControllerBase
    {
        private readonly SisTalleresContext _context;

        public TalleresController(SisTalleresContext context)
        {
            _context = context;
        }

        // GET: api/talleres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Taller>>> GetTalleres()
        {
            return await _context.Talleres.ToListAsync();
        }

        // GET: api/talleres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Taller>> GetTaller(int id)
        {
            var taller = await _context.Talleres.FindAsync(id);

            if (taller == null)
            {
                return NotFound(new { mensaje = $"No se encontró el taller con id {id}" });
            }

            return taller;
        }

        // POST: api/talleres
        [HttpPost]
        public async Task<ActionResult<Taller>> PostTaller(Taller taller)
        {
            _context.Talleres.Add(taller);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaller), new { id = taller.TallerId }, taller);
        }

        // PUT: api/talleres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaller(int id, Taller taller)
        {
            if (id != taller.TallerId)
            {
                return BadRequest(new { mensaje = "El id de la URL no coincide con el id del taller enviado" });
            }

            _context.Entry(taller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TallerExiste(id))
                {
                    return NotFound(new { mensaje = $"No se encontró el taller con id {id}" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/talleres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaller(int id)
        {
            var taller = await _context.Talleres.FindAsync(id);
            if (taller == null)
            {
                return NotFound(new { mensaje = $"No se encontró el taller con id {id}" });
            }

            _context.Talleres.Remove(taller);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TallerExiste(int id)
        {
            return _context.Talleres.Any(e => e.TallerId == id);
        }
    }
}