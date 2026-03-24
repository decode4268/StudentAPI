namespace StudentAPI.Middleware
{
    // Request => Middleware => Controller => Middleware => Response
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"Request Path:{context.Request.Path}");

            await _next(context);

            Console.WriteLine($"Response Status:{context.Response.StatusCode}");
        }
    }
}
