using Backend.Api.MasterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Api.DAL.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(c => c.Id);
    }
}

public class ManufactorerConfiguration : IEntityTypeConfiguration<ManufacturerEntity>
{
    public void Configure(EntityTypeBuilder<ManufacturerEntity> builder)
    {
        builder.HasKey(c => c.Id);
    }
}

public class ParametersConfiguration : IEntityTypeConfiguration<ParameterEntity>
{
    public void Configure(EntityTypeBuilder<ParameterEntity> builder)
    {
        builder.HasKey(c => c.Id);
    }
}