using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.ImageServices;
using System.Net;
using AutoMapper;
using Lagalt_Backend.Models.Dto.Image;

namespace Lagalt_Backend.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        // GET: api/Image
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages()
        {
            return Ok(
                _mapper.Map<List<ImageDTO>>(
                await _imageService.GetAllAsync()));
        }

        // GET: api/Image/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<ImageDTO>(
                    await _imageService.GetByIdAsync(id)));
            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
            }
        }

        // PUT: api/Image/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutImage(int id, ImagePutDTO imageDto)
        {
            if (id != imageDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _imageService.UpdateAsync(_mapper.Map<Image>(imageDto));
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
            }
        }

        // POST: api/Image
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagePostDTO>> PostImage(ImagePostDTO imageDto)
        {
            Image image = _mapper.Map<Image>(imageDto);
            await _imageService.AddAsync(image);
            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        // DELETE: api/Image/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                await _imageService.DeleteByIdAsync(id);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.NotFound
                    });
            }
        }
    }
}
