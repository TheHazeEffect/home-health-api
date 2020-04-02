using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.data.tables;

namespace HomeHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly HomeHealthDbContext _context;

        public ServiceController(HomeHealthDbContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetService()
        {
            return await _context.Service.ToListAsync();
        }

        [HttpGet("{ServiceId}/Professionals")]
        public async Task<ActionResult<IEnumerable<Service>>> getProfessionalsForService(int ServiceId){

            return await _context.Service
                .Where(S => S.ServiceId == ServiceId)
                .Include("Professionals")
                .ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetServices(int id)
        {
            var services = await _context.Service.FindAsync(id);

            if (services == null)
            {
                return NotFound();
            }

            return services;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServices(int id, Service services)
        {
            if (id != services.ServiceId)
            {
                return BadRequest();
            }

            _context.Entry(services).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Service>> PostServices(Service services)
        {
            _context.Service.Add(services);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServices", new { id = services.ServiceId }, services);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> DeleteServices(int id)
        {
            var services = await _context.Service.FindAsync(id);
            if (services == null)
            {
                return NotFound();
            }

            _context.Service.Remove(services);
            await _context.SaveChangesAsync();

            return services;
        }

        private bool ServicesExists(int id)
        {
            return _context.Service.Any(e => e.ServiceId == id);
        }
    }
}
