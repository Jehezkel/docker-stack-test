namespace Backend.Api.DAL.Entities;

public class ProductEntity : UserOwnedEntity
{
    public string Name { get; set; } = default!;
    public string EAN { get; set; } = default!;
    public MeasurementsEntity Measurements { get; set; } = default!;
}

public abstract class AuditableEntity
{
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public string? UpdatedBy { get; set; } 
    public DateTime? UpdatedOn { get; set; }
}

public abstract class UserOwnedEntity : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}