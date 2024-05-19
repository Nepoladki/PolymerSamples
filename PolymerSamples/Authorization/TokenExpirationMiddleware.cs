namespace PolymerSamples.Authorization
{
    public class TokenExpirationMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized &&
                context.Response.Headers.ContainsKey("Token-Expired"))
                context.Response.Redirect("api/auth/Auth/refresh");
            
        }
    }
}
