using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lagalt_Backend.Controllers
{
    [Route("messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
        {
            return Ok(
                await _messageService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            try
            {
                return Ok(await _messageService.GetByIdAsync(id));

            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddMessage(Message message)
        {
            await _messageService.AddAsync(message);
            return CreatedAtAction("GetMessageById", new { id = message.Id }, message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessage(int id, Message message)
        {
            try
            {
                await _messageService.UpdateAsync(message);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(
                     new ProblemDetails()
                     {
                         Detail = ex.Message,
                         Status = ((int)HttpStatusCode.NoContent)
                     });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messageService.DeleteByIdAsync(id);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NoContent)
                    });
            }
        }
    }
}
