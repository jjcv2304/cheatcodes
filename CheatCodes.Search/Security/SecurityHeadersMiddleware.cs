using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

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

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      context.Response.Headers.Add(
        "Content-Security-Policy","default-src 'self'; script-src 'self'; script-src-elem 'self'; style-src 'self'; style-src-elem 'self'; style-src-attr 'self'; img-src 'self'; connect-src 'self'; media-src 'self'; frame-src 'none'; frame-ancestors 'none'; base-uri 'self'");
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
