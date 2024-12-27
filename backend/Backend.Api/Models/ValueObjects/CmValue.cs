namespace Backend.Api.Models.ValueObjects;

public record CmValue
{
   public decimal Value { get; init; } = 0;
   public CmValue() { }
   public CmValue(decimal value)
   {
      ArgumentOutOfRangeException.ThrowIfNegative(value, "Centimeters value must be positive");
      Value = value;
   }

   public static implicit operator CmValue(decimal value) => new(value);
   public static implicit operator decimal(CmValue value) => value.Value;
}
