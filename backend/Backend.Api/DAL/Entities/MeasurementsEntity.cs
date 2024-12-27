namespace Backend.Api.DAL.Entities;

public class MeasurementsEntity
{
   public Guid ProductId { get; set; }
   public ProductEntity ProductEntity { get; set; } = null!;
   public decimal HeightInCm { get; set; }
   public decimal WidthInCm { get; set; }
   public decimal LengthInCm { get; set; }
   public decimal WeightInKg { get; set; }
}


