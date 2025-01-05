namespace Backend.Api.DAL.Entities;

public class CategoryEntity : UserOwnedEntity, IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}