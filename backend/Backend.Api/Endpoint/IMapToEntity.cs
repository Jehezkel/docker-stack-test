namespace Backend.Api.Endpoint;

public interface IMapToEntity<in TDto, out TEntity>
{
    TEntity MapToEntity(TDto dto);
}