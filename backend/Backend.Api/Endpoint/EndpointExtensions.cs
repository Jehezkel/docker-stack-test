using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Backend.Api.Endpoint;

public static class EndpointExtensions
{
   public static IServiceCollection AddEndpoints(this IServiceCollection services)
   {
      services.AddEndpoints(Assembly.GetExecutingAssembly());
      return services;
   }

   private static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
   {
      ServiceDescriptor[] serviceDescriptors = [.. assembly
          .DefinedTypes
          .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                         type.IsAssignableTo(typeof(IEndpoint)))
          .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))];

      services.TryAddEnumerable(serviceDescriptors);
      return services;
   }

   public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
   {
      IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
      IEndpointRouteBuilder endpointRouteBuilder = routeGroupBuilder is null ? app : routeGroupBuilder;

      foreach (IEndpoint endpoint in endpoints)
      {
         endpoint.MapEndpoint(endpointRouteBuilder);
      }


      return app;
   }
}
