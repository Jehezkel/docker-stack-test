using Backend.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Api.DAL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
   public void Configure(EntityTypeBuilder<ProductEntity> builder)
   {

      builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
      builder.Property(p => p.EAN).HasMaxLength(20).IsRequired();

      builder.HasOne(p => p.Measurements)
         .WithOne(p => p.ProductEntity)
         .HasForeignKey<MeasurementsEntity>(m => m.ProductId);
   }
}

public class MeasurementsConfiguration : IEntityTypeConfiguration<MeasurementsEntity>
{
   public void Configure(EntityTypeBuilder<MeasurementsEntity> builder)
   {
      builder.ToTable("Measurements");
      builder.HasKey(m => m.ProductId);
   }
}
