using Backend.Api.DAL;
using Backend.Api.MasterData;

namespace Backend.Api.Endpoint;

public static class GenericEndpointsExtensions
{
    private static async Task<List<T>> GetHandler<T>(IGenericRepository<T> repository)
        where T : class, IIdentifiable<Guid>
        => await repository.GetEntities();

    private static async Task<IResult> GetByIdHandler<T>(IGenericRepository<T> repository, Guid id)
        where T : class, IIdentifiable<Guid>
    {
        var results = await repository.GetEntityById(id);
        if (results is null) return Results.NotFound();
        return Results.Ok(results);
    }


    private static async Task<IResult> AddHandler<T, TDto>(IGenericRepository<T> repository, TDto request)
        where T : class, IIdentifiable<Guid>
        where TDto : class, IMapToEntity<TDto, T>
    {
        var entity = request.MapToEntity(request);
        var x = await repository.InsertEntity(entity);
        return Results.Created();
    }

    private static async Task<IResult> UpdateHandler<T, TDto>(IGenericRepository<T> repository, TDto request, Guid id)
        where T : class, IIdentifiable<Guid>
        where TDto : class, IMapToEntity<TDto, T>
    {
        var entity = request.MapToEntity(request);
        await repository.UpdateEntity(id, request);
        return Results.NoContent();
    }


    private static async Task<IResult> DeleteHandler<T>(IGenericRepository<T> repository, Guid id)
        where T : class, IIdentifiable<Guid>
    {
        await repository.DeleteEntity(id);
        return Results.Accepted();
    }

    public static void AddCrudEndpoints<TDto, TEntity>(this RouteGroupBuilder builder)
        where TDto : class, IMapToEntity<TDto, TEntity>
        where TEntity : class, IIdentifiable<Guid>
    {
        builder.MapPost("", AddHandler<TEntity, TDto>);
        builder.MapGet("", GetHandler<TEntity>);
        builder.MapGet("{id:Guid}", GetByIdHandler<TEntity>);
        builder.MapDelete("{id:Guid}", DeleteHandler<TEntity>);
        builder.MapPut("{id:Guid}", UpdateHandler<TEntity, TDto>);
    }
}