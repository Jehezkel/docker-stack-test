namespace Backend.Api.DAL.Entities;

public class ManufacturerEntity : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}