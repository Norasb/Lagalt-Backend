﻿using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Message;
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
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetAllMessages()
        {
            return Ok(
                _mapper.Map<List<MessageDto>>(
                await _messageService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> GetMessageById(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<MessageDto>(
                    await _messageService.GetByIdAsync(id)));

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
        public async Task<ActionResult> AddMessage(MessagePostDto postMessage)
        {
            Message message = new Message()
            {
                DOC = DateTime.Now,
                Text = postMessage.Text,
                UserId = postMessage.UserId,
            };

            await _messageService.AddAsync(_mapper.Map<Message>(message));
            return CreatedAtAction("GetMessageById", new { id = message.Id }, message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessage(int id, MessagePutDto putMessage)
        {
            Message existingMessage = _messageService.GetByIdAsync(id).Result;

            existingMessage.Id = id;
            existingMessage.Text = putMessage.Text;
            existingMessage.DOC = DateTime.Now;

            try
            {
                await _messageService.UpdateAsync(_mapper.Map<Message>(existingMessage));
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
