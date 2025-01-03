using Backend.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Api.DAL.Configurations;

public class ParametersConfiguration : IEntityTypeConfiguration<ParameterEntity>
{
    public void Configure(EntityTypeBuilder<ParameterEntity> builder)
    {
        builder.HasKey(c => c.Id);
    }
}