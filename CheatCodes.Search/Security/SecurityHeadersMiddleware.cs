using System.Threading.Tasks;
using Api.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CheatCodes.Search.Security
{
  public static class IApplicationBuilderExtensions
  {
    public static void UseSecurityHeaders(
      this IApplicationBuilder app)
    {
      app.UseMiddleware<SecurityHeadersMiddleware>();
    }
  }

  public class SecurityHeadersMiddleware
  {
    private readonly RequestDelegate next;
    private readonly IOptions<MyConfig> _config;

    public SecurityHeadersMiddleware(RequestDelegate next, IOptions<MyConfig> config)
    {
      this.next = next;
      _config = config;
    }

    public async Task Invoke(HttpContext context)
    {
      if (_config.Value.Environment == Environments.Development.ToString())
      {

      }
      else
      {
        context.Response.Headers.Add(
          "Content-Security-Policy", "default-src 'self'; script-src 'self'; script-src-elem 'self'; style-src 'self'; style-src-elem 'self'; style-src-attr 'self'; img-src 'self'; connect-src 'self'; media-src 'self'; frame-src 'none'; frame-ancestors 'none'; base-uri 'self'");
      }
      //require-sri-for script style
      context.Response.Headers.Add(
        "Feature-Policy", "camera 'none'");
      context.Response.Headers.Add(
        "X-Content-Type-Options", "nosniff");
      context.Response.Headers.Add(
        "Referrer-Policy", "no-referrer");
      await next(context);
    }

  }
}
