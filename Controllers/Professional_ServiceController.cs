using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.data.tables;
using HomeHealth.Entities;

namespace HomeHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Professional_ServiceController : ControllerBase
    {
        private readonly HomeHealthDbContext _context;

        public Professional_ServiceController(HomeHealthDbContext context)
        {
            _context = context;
        }

        // GET: api/Professional_Service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professional_Service>>> GetProfessional_Service()
        {
            return await _context.Professional_Service.ToListAsync();
        }

        // GET: api/Professional_Service/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professional_Service>> GetProfessional_Service(int id)
        {
            var professional_Service = await _context.Professional_Service.FindAsync(id);

            if (professional_Service == null)
            {
                return NotFound();
            }

            return professional_Service;
        }
        [HttpGet("profile/{id}")]
        public async Task<ActionResult<ProfileServicesDto>> GetProfessional_Service(string id)
        {
            var services = await _context.Professional_Service
                .Include("Professional")
                .Include("Service")
                .Where( PS => PS.Professional.userId == id )
                .Select( PS => new ProfileServicesDto{
                    id = (int)PS.Professional_ServiceId,
                    Name = PS.Service.ServiceName,
                    Cost = (float)PS.ServiceCost

                })
                .ToListAsync();

            if (services == null)
            {
                return NotFound();
            }

            return Ok(services);
        }

        [HttpPost("profile")]
        public async Task<ActionResult<ProfileServicesDto>> AddProfessional_Service(AddProfServiceDto ProfServDto)
        {
            var Prof = await _context.Professional
                .Where( PS => PS.userId == ProfServDto.id ).FirstOrDefaultAsync();

            if (Prof == null)
            {
                return NotFound();
            }

            var newProfService = new Professional_Service {
                ServiceId = ProfServDto.ServiceId,
                ServiceCost = ProfServDto.ServiceCost
            };

            Prof.Prof_services.Add(newProfService);


            _context.Professional.Update(Prof);
            
            await _context.SaveChangesAsync();


            var service =await  _context.Service
                .Where(S => S.ServiceId == ProfServDto.ServiceId)
                .FirstOrDefaultAsync();

            var payload = new ProfileServicesDto{
                Cost = (float)newProfService.ServiceCost,
                Name = service.ServiceName,
                id = newProfService.Professional_ServiceId
            };
                

            return Ok(payload);
        }

        // PUT: api/Professional_Service/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessional_Service(int id, Professional_Service professional_Service)
        {
            if (id != professional_Service.Professional_ServiceId)
            {
                return BadRequest();
            }

            _context.Entry(professional_Service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Professional_ServiceExists(id))
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

        // POST: api/Professional_Service
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Professional_Service>> PostProfessional_Service(Professional_Service professional_Service)
        {
            _context.Professional_Service.Add(professional_Service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessional_Service", new { id = professional_Service.Professional_ServiceId }, professional_Service);
        }

        // DELETE: api/Professional_Service/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Professional_Service>> DeleteProfessional_Service(int id)
        {

            Console.WriteLine("-------------------------------------");
            Console.WriteLine(id);
            Console.WriteLine("-------------------------------------");

            var professional_Service = await _context.Professional_Service
                .Where( S => S.Professional_ServiceId  == id)
                .FirstOrDefaultAsync();
            if (professional_Service == null)
            {
                return NotFound();
            }

            _context.Professional_Service.Remove(professional_Service);
            await _context.SaveChangesAsync();

            return professional_Service;
        }

        private bool Professional_ServiceExists(int id)
        {
            return _context.Professional_Service.Any(e => e.Professional_ServiceId == id);
        }
    }
}
