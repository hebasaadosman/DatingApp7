
using API.Data;
using API.Entities;
using API.Extensions;
using API.Middleware;
using API.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


//     });
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>(); // UseMiddleware

// Configure the HTTP request pipeline.

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.AllowCredentials()// AllowCredentials allows the SignalR hub to send the JWT token to the server.
.WithOrigins("https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence"); // MapHub hub to the /hubs/presence endpoint.
app.MapHub<MessageHub>("hubs/message"); // MapHub hub to the /hubs/message endpoint.
using (var scope = app.Services.CreateScope()) // CreateScope
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<DataContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        await context.Database.MigrateAsync();
    //    context.Connections.RemoveRange(context.Connections);//remove all connections from db on startup // for small scale app
        await context.Database.MigrateAsync();
        await context.Database.ExecuteSqlRawAsync("DELETE FROM  [Connections]");//remove all connections from db on startup // for large scale app
       await Seed.SeedDataUsers(userManager,roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
    }
}

app.Run();
