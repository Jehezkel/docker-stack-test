using Backend.Api.MasterData;

namespace Backend.Api.DAL;

public interface IGenericRepository<T> where T : class, IIdentifiable<Guid>
{
    Task<List<T>> GetEntities();
    Task<T?> GetEntityById(Guid id);
    Task DeleteEntity(Guid id);
    Task UpdateEntity(Guid id, Object dto);
    Task<Guid> InsertEntity(T entity);
}