using System.Text.Json;

using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly IHostEnvironment env;

    public ExceptionMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        this.next = next;
        this.logger = logger;
        this.env = env;
    }

    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
    {
        try 
        {
            await  next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
        
            var resposne = env.IsDevelopment() ? 
            new ApiException(context.Response.StatusCode,ex.Message,
            ex.StackTrace?.ToString() ?? string.Empty) : 
            new ApiException(context.Response.StatusCode,ex.Message,
            "Internal Server Error");

            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var json = JsonSerializer.Serialize(resposne,options);
            
            await context.Response.WriteAsync(json);
        }
    }

}
