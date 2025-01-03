namespace Backend.Api.DAL.Entities;

public class ParameterEntity : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}