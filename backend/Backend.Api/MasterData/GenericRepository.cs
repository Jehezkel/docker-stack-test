using Backend.Api.DAL;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.MasterData;

public class GenericRepository<T>(AppDbContext appDbContext)
    : IGenericRepository<T> where T : class, IIdentifiable<Guid>
{
    public async Task<List<T>> GetEntities()
    {
        return await appDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityById(Guid id)
    {
        return await appDbContext.Set<T>().FindAsync(id);
    }

    public async Task DeleteEntity(Guid id)
    {
        var entityToDelete = await GetEntityById(id);
        if (entityToDelete != null)
        {
            appDbContext.Set<T>().Remove(entityToDelete);
            await appDbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateEntity(Guid id, Object  dto)
    {
        var entityToUpdate = await GetEntityById(id);
        if (entityToUpdate != null)
        {
            appDbContext.Entry(entityToUpdate).CurrentValues.SetValues(dto);
            await appDbContext.SaveChangesAsync();
        }
    }

    public async Task<Guid> InsertEntity(T entity)
    {
        await appDbContext.Set<T>().AddAsync(entity);
        await appDbContext.SaveChangesAsync();
        return entity.Id;
    }
}