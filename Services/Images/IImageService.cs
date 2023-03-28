using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.ImageServices
{
    public interface IImageService : ICrudService<Image, int>
    {
        public Task<bool> ImageExists(int id);
        public Task<ICollection<Image>> GetImagesByIdAsync(List<int> imageIds);
    }
}
