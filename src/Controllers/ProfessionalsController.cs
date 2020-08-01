using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.Data.Tables;

namespace HomeHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalsController : ControllerBase
    {
        private readonly HomeHealthDbContext _context;

        public ProfessionalsController(HomeHealthDbContext context)
        {
            _context = context;
        }

        // GET: api/Professionals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professionals>>> GetProfessional()
        {
            return await _context.Professional
                .Include("user")
                .ToListAsync();
        }

        // GET: api/Professionals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professionals>> GetProfessionals(int id)
        {
            var professionals = await _context.Professional.FindAsync(id);

            if (professionals == null)
            {
                return NotFound();
            }

            return professionals;
        }
        [HttpGet("{id}/details")]
        public async Task<ActionResult<Professionals>> GetProfessionalsDetails(int id)
        {
            var professionals = await _context.Professional
                .Include("Prof_services.Service")
                .Include("user")
                .FirstOrDefaultAsync( p => p.ProfessionalsId == id);

            if (professionals == null)
            {
                return NotFound();
            }

            return professionals;
        }

        // PUT: api/Professionals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessionals(int id, Professionals professionals)
        {
            if (id != professionals.ProfessionalsId)
            {
                return BadRequest();
            }

            _context.Entry(professionals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionalsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Professionals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Professionals>> PostProfessionals(Professionals professionals)
        {
            _context.Professional.Add(professionals);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessionals", new { id = professionals.ProfessionalsId }, professionals);
        }

        // DELETE: api/Professionals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Professionals>> DeleteProfessionals(int id)
        {
            var professionals = await _context.Professional.FindAsync(id);
            if (professionals == null)
            {
                return NotFound();
            }

            _context.Professional.Remove(professionals);
            await _context.SaveChangesAsync();

            return professionals;
        }

        private bool ProfessionalsExists(int id)
        {
            return _context.Professional.Any(e => e.ProfessionalsId == id);
        }
    }
}
