namespace Backend.Api.DAL;

public interface IIdentifiable<TKey>
{
    TKey Id { get; set; }
}