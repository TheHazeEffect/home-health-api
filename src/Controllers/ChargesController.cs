using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.Data.Tables;

namespace HomeHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase
    {
        private readonly HomeHealthDbContext _context;

        public ChargesController(HomeHealthDbContext context)
        {
            _context = context;
        }

        // GET: api/Charges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Charges>>> GetCharge()
        {
            return await _context.Charge.ToListAsync();
        }

        // GET: api/Charges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Charges>> GetCharges(int id)
        {
            var charges = await _context.Charge.FindAsync(id);

            if (charges == null)
            {
                return NotFound();
            }

            return charges;
        }

        // PUT: api/Charges/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharges(int id, Charges charges)
        {
            if (id != charges.ChargeId)
            {
                return BadRequest();
            }

            _context.Entry(charges).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChargesExists(id))
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

        // POST: api/Charges
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Charges>> PostCharges(Charges charges)
        {
            _context.Charge.Add(charges);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharges", new { id = charges.ChargeId }, charges);
        }

        // DELETE: api/Charges/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Charges>> DeleteCharges(int id)
        {
            var charges = await _context.Charge.FindAsync(id);
            if (charges == null)
            {
                return NotFound();
            }

            _context.Charge.Remove(charges);
            await _context.SaveChangesAsync();

            return charges;
        }

        private bool ChargesExists(int id)
        {
            return _context.Charge.Any(e => e.ChargeId == id);
        }
    }
}
