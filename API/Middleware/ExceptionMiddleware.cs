using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
       {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context) // InvokeAsync method
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) // Catch block
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Status code
                var response = _env.IsDevelopment() // Response
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) // If development
                    : new ApiException(context.Response.StatusCode, "Internal Server Error"); // If production
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; // Options
                var json = JsonSerializer.Serialize(response, options); // Json
                await context.Response.WriteAsync(json); // WriteAsync
            }
        }
    }
}