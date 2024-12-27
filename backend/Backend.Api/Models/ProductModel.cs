namespace Backend.Api.Models;

public class ProductModel
{
   public ProductModel(string name, string ean)
   {
      if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
      {
         throw new ArgumentException("Product name must be not null and max 200 characters long");
      }

      Name = name;

      if (string.IsNullOrWhiteSpace(ean) || ean.Length < 8 || ean.Length > 20)
      {
         throw new ArgumentException("EAN must be between 8-20 characters long");
      }
      EAN = ean;
   }
   public Guid ProductId { get; set; }
   public string Name { get; set; }
   public string EAN { get; set; }
   public MeasurementsModel Measurements { get; set; } = new MeasurementsModel();
}
