using Backend.Api.DAL;
using Backend.Api.Endpoint;
using Backend.Api.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.MasterData;

public class GetMasterDataRecords
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var mapGroup = app.MapGroup("masterdata");
            var categories = mapGroup.MapGroup("categories").WithTags("category");
            // categories.MapGet("", GetHandler<CategoryEntity, Guid>);
            // //Post/update needs entity without ID...
            // categories.MapPost("",
            //     (IGenericRepository<CategoryEntity, Guid> repo, CategoryDTO request) =>
            //     {
            //         return AddHandler<CategoryEntity, Guid>(repo, entity);
            //     });
            // categories.MapPut("{id:Guid}", UpdateHandler<CategoryEntity, Guid>);
            // categories.MapDelete("", DeleteHandler<CategoryEntity, Guid>);
            categories.AddCrudEndpoints<CategoryDTO, CategoryEntity>();
            // var manufacturers = mapGroup.MapGroup("manufacturers").WithTags("manufacturer");
            // var parameters = mapGroup.MapGroup("parameters").WithTags("parameter");
        }

        // private async Task<List<T>> GetHandler<T>(IGenericRepository<T> repository)
        //     where T : class, IIdentifiable<Guid>
        //     => await repository.GetEntities();
        //
        // private async Task<IResult> GetByIdHandler<T>(IGenericRepository<T> repository, Guid id)
        //     where T : class, IIdentifiable<Guid>
        // {
        //     var results = await repository.GetEntityById(id);
        //     if (results is null) return Results.NotFound();
        //     return Results.Ok(results);
        // }
        //
        //
        // private async Task<IResult> AddHandler<T, TDto>(IGenericRepository<T> repository, TDto request)
        //     where T : class, IIdentifiable<Guid>
        //     where TDto : class, IMapToEntity<TDto, T>
        // {
        //     var entity = request.MapToEntity(request);
        //     var x = await repository.InsertEntity(entity);
        //     return Results.Created();
        // }
        //
        // private async Task<IResult> UpdateHandler<T, TDto>(IGenericRepository<T> repository, TDto request, Guid id)
        //     where T : class, IIdentifiable<Guid>
        //     where TDto : class, IMapToEntity<TDto, T>
        // {
        //     var entity = request.MapToEntity(request);
        //     await repository.UpdateEntity(id, entity);
        //     return Results.NoContent();
        // }
        //
        //
        // private async Task<IResult> DeleteHandler<T>(IGenericRepository<T> repository, Guid id)
        //     where T : class, IIdentifiable<Guid>
        // {
        //     await repository.DeleteEntity(id);
        //     return Results.Accepted();
        // }
    }
}

public static class EnpointExtensions
{
    public static void AddCrudEndpoints<TDto, TEntity>(this RouteGroupBuilder builder)
        where TDto : class, IMapToEntity<TDto, TEntity>
        where TEntity : class, IIdentifiable<Guid>
    {
        // builder.MapPost("", (IGenericRepository<TEntity> repository) => { });
        builder.MapGet("", async (IGenericRepository<TEntity> repo) => { return await repo.GetEntities(); });
        // builder.MapGet("{id:Guid}", GetByIdHandler<TEntity>);
        // builder.MapDelete("{id:Guid}", DeleteHandler<TEntity>);
        // builder.MapPut("{id:Guid}", UpdateHandler<TEntity, TDto>);
    }
}

public class CategoryDTO : IMapToEntity<CategoryDTO, CategoryEntity>
{
    public string Name { get; set; } = default!;
    public CategoryEntity MapToEntity(CategoryDTO dto) => new CategoryEntity { Name = dto.Name };
}

public interface IMapToEntity<TDto, TEntity>
{
    TEntity MapToEntity(TDto dto);
}

public class CategoryEntity : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class ManufacturerEntity : IIdentifiable<Guid>
{
    public Guid Id { get; }
    public string Name { get; set; } = null!;
}

public class ParameterEntity : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public interface IGenericRepository<T> where T : class, IIdentifiable<Guid>
{
    Task<List<T>> GetEntities();
    Task<T?> GetEntityById(Guid id);
    Task DeleteEntity(Guid id);
    Task UpdateEntity(Guid id, T entity);
    Task<Guid> InsertEntity(T entity);
}

public class GetIdType<T> where T : class, IIdentifiable<object>
{
    private GetIdType()
    {
    }

    public static Type IdType => typeof(T).GetInterface(typeof(IIdentifiable<>).Name)!.GenericTypeArguments[0];
}

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

    public async Task UpdateEntity(Guid id, T entity)
    {
        var entityToUpdate = await GetEntityById(id);
        if (entityToUpdate != null)
        {
            appDbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
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

public interface IIdentifiable<out TKey>
{
    TKey Id { get; }
}