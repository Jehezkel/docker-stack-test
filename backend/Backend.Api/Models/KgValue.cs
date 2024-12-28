namespace Backend.Api.Models;

public record KgValue
{

   public decimal Value { get; init; } = 0;
   public KgValue() { }
   public KgValue(decimal value)
   {
      ArgumentOutOfRangeException.ThrowIfNegative(value, "Kilograms value must be positive.");
      Value = value;
   }

   public static implicit operator KgValue(decimal value) => new(value);
   public static implicit operator decimal(KgValue value) => value.Value;
}

