using Microsoft.EntityFrameworkCore;

namespace Backend.Api.DAL;

public static class ServicesExtensions
{

   public static void AddDatabase(this IServiceCollection services, IConfiguration config)
   {
      var connStringName = "AppDb";
      var connectionString = config.GetConnectionString(connStringName) ?? throw new ArgumentNullException(connStringName);

      services.AddDbContext<AppDbContext>(bOpt => bOpt.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

   }
}
