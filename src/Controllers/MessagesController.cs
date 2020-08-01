using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.Entities;
using HomeHealth.Data.Tables;

namespace HomeHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly HomeHealthDbContext _context;

        public MessagesController(HomeHealthDbContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Messages>>> GetMessage()
        {
            return await _context.Message.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Messages>> GetMessages(int id)
        {
            // var Token =  await HttpContext.GetTokenAsync("access_token");

            // if(Token == null){
            //     return Unauthorized();
            // }

            var messages = await _context.Message.FindAsync(id);

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        [HttpGet("Chat/{id}")]
        public async Task<ActionResult<IEnumerable<ChatHistory>>> GetChatMessages(string id)
        {
            var messages = await _context.Message
                .Where( M => M.ReceiverId == id || M.SenderId == id)
                .Include("Sender")
                .Include("Reciever")
                .ToListAsync();

            var History = new HashSet<ChatHistory>();
            foreach (var message in messages)
            {
                var found = History.FirstOrDefault(H => 
                    H.Id == message.SenderId || 
                    H.Id == message.ReceiverId
                );

                //check if already in list
                if(found != null)
                    continue;
                
                           
                if(message.ReceiverId == id){
                    
                    if(message.SenderId != null){
                        var userinfo = new ChatHistory{
                            Id = message.SenderId,
                            FirstName = message.Sender.FirstName,
                            LastName = message.Sender.LastName,
                            Email = message.Sender.Email,
                            Conversation = new HashSet<Messages>()
                        };
                        History.Add(userinfo);
                    }

                }else if(message.SenderId == id){

                    if(message.ReceiverId != null){
                        var userinfo = new ChatHistory{
                            Id = message.ReceiverId,
                            FirstName = message.Reciever.FirstName,
                            LastName = message.Reciever.LastName,
                            Email = message.Reciever.Email,
                            Conversation = new HashSet<Messages>()

                        };
                        History.Add(userinfo);
                    }

                }

            }

            foreach (var item in History)
            {
                var MessageList = messages.FindAll( m => m.SenderId == id && m.ReceiverId == item.Id ||
                m.ReceiverId == id && m.SenderId == item.Id);

                // MessageList.Sort((x, y) => DateTime.Compare(x.TimeStamp, y.TimeStamp));
                foreach (var item2 in MessageList)
                {
                    item2.Sender = null;
                    item2.Reciever = null;
                    item.Conversation.Add(item2);
                }
                    
            }


            return Ok(History);
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessages(int id, Messages messages)
        {
            if (id != messages.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(messages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagesExists(id))
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

        // POST: api/Messages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Messages>> PostMessages(Messages messages)
        {
            try {

                messages.TimeStamp = DateTime.Now;
                _context.Message.Add(messages);
                await _context.SaveChangesAsync();


                return Ok( new { message = "Message Sent!",messages});
         
            }catch(Exception ex) {
                Console.WriteLine(ex);
                return BadRequest(new { message = "Something went wrong",messages});

            }
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Messages>> DeleteMessages(int id)
        {
            var messages = await _context.Message.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }

            _context.Message.Remove(messages);
            await _context.SaveChangesAsync();

            return messages;
        }

        private bool MessagesExists(int id)
        {
            return _context.Message.Any(e => e.MessageId == id);
        }
    }
}
