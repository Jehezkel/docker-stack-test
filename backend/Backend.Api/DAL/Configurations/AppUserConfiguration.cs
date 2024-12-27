using Backend.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Api.DAL.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUserEntity>
{
   public void Configure(EntityTypeBuilder<AppUserEntity> builder)
   {
      builder.HasKey(u => u.AppUserId);

      builder.Property(u => u.Subject).HasMaxLength(128);
      builder.HasIndex(u => u.Subject);

      builder.Property(u => u.Email).HasMaxLength(128);
      builder.Property(u => u.Name).HasMaxLength(128);
   }

}
