using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.ImageServices
{
    public interface IImageService : ICrudService<Image, int>
    {
        /// <summary>
        /// Check if the image exists in the database by ID.
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <returns>True if the image exists, false if not.</returns>
        public Task<bool> ImageExists(int id);
    }
}
