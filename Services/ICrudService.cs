namespace Lagalt_Backend.Services
{
    public interface ICrudService<T, ID>
    {
        /// <summary>
        /// Gets all the instances of an entity
        /// </summary>
        /// <returns>A collection of the entity</returns>
        Task<ICollection<T>> GetAllAsync();
        /// <summary>
        /// Gets a specific instance of an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single entity</returns>
        Task<T> GetByIdAsync(ID id);
        /// <summary>
        /// Creates a new instance of an entity
        /// </summary>
        /// <param name="obj"></param>
        Task AddAsync(T obj);
        /// <summary>
        /// Updates an instance of an entity
        /// </summary>
        /// <param name="obj"></param>
        Task UpdateAsync(T obj);
        /// <summary>
        /// Deletes an instance of an entity
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(ID id);
    }
}
