using OutOfOfficeApp.Exceptions;

namespace OutOfOfficeApp.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequest)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
