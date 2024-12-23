using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Backend.Api.Security;

public static class ServicesExtensions
{
   public static void AddJwt(this IServiceCollection services, IConfiguration config)
   {

      var authString = "Jwt:Authority";
      var audienceString = "Jwt:Audience";

      var authority = config.GetValue<string>(authString) ?? throw new ArgumentNullException(authString);
      var audience = config.GetValue<string>(audienceString) ?? throw new ArgumentNullException(audienceString);

      services.AddAuthentication(opt =>
      {
         opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(opt =>
      {
         opt.Authority = authority;
         opt.Audience = audience;
      });

      services.AddAuthorization();


   }
}

