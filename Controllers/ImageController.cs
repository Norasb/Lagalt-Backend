using Microsoft.AspNetCore.Mvc;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Services.ImageServices;
using System.Net;
using AutoMapper;
using Lagalt_Backend.Models.Dto.Image;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace Lagalt_Backend.Controllers
{
    [Route("api/images")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// <summary>
        /// Get all images in the database.
        /// </summary>
        /// <returns>List of images</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages()
        {
            return Ok(
                _mapper.Map<List<ImageDTO>>(
                await _imageService.GetAllAsync()));
        }

        // GET: api/Image/5
        /// <summary>
        /// Gets a specific image from the database by ID.
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <returns>ImageDTO if the request is successful.
        /// NotFound if the request fails.</returns>
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
        /// <summary>
        /// Update an image in the database by ID.
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <param name="imageDto">ImageDTO</param>
        /// <returns>BadRequest if the ID in the DTO does not match the ID from the URL.
        /// NoContent if the image is updated successfully.
        /// NotFound if the update fails.</returns>
        [HttpPut("{id}")]
        [Authorize]
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
        /// <summary>
        /// Add an image to the database.
        /// </summary>
        /// <param name="imageDto">ImageDTO</param>
        /// <returns>ImagePostDTO</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ImagePostDTO>> PostImage(ImagePostDTO imageDto)
        {
            Image image = _mapper.Map<Image>(imageDto);
            await _imageService.AddAsync(image);
            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        // DELETE: api/Image/5
        /// <summary>
        /// Delete an image by ID.
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <returns>NoContent if the image is deleted successfully.
        /// NotFound if the request fails.</returns>
        [HttpDelete("{id}")]
        [Authorize]
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
