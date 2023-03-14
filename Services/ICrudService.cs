namespace Lagalt_Backend.Services
{
    public interface ICrudService<T, ID>
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(ID id);
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(ID id);

    }
}
