using MediatR;

namespace Backend.Api;

public static class AppBuilderExtensions
{
   public static RouteHandlerBuilder MapGetToHandler(this WebApplication app, string pattern, IBaseRequest request) =>
      app.MapGet(pattern, (IMediator mediator) => mediator.Send(request));
}