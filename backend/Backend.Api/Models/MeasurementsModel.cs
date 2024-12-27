using Backend.Api.Models.ValueObjects;

namespace Backend.Api.Models;

public class MeasurementsModel
{
   public CmValue HeightInCm { get; set; } = new();
   public CmValue WidthInCm { get; set; } = new();
   public CmValue LengthInCm { get; set; } = new();
   public KgValue WeightInKg { get; set; } = new();
}


