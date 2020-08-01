using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.Data.Tables;
using HomeHealth.Interfaces;


namespace HomeHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly HomeHealthDbContext _context;
        private readonly IEmailService _emailSender;


        public CommentsController(HomeHealthDbContext context,IEmailService emailSender)
        {
            _emailSender = emailSender;
            _context = context;
        }

        // GET: api/Charges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GeComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Charges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpGet("professional/{id}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetProfComment(int id)
        {
            var comment = await _context.Comments
                .Include("Sender")
                .Where( C => C.ProfessionalId == id)
                .ToListAsync();

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Charges/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comments comment)
        {
            if (id != comment.CommentsId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

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
        public async Task<ActionResult<Charges>> PostComment(Comments Comment)
        {
            try{

                Comment.TimeStamp = DateTime.Now;
                Console.WriteLine("-----------------------------------" + Comment.Content);
                Console.WriteLine("-----------------------------------" + Comment.ProfessionalId);
                Console.WriteLine("-----------------------------------" + Comment.SenderId);
                _context.Comments.Add(Comment);
                await _context.SaveChangesAsync();


                await _emailSender.SendEmailUsingProfId(
                    Comment.ProfessionalId,
                    "New Comment Notification",
                    $"Someone commented on your Profile, Check it out"
                );

                return Ok( new {message =  "Comment Added",Comment});


            }catch(Exception ex){
                Console.WriteLine(ex);

                return BadRequest(Comment);


            }
        }

      

        private bool ChargesExists(int id)
        {
            return _context.Comments.Any(e => e.CommentsId == id);
        }

    }
}
