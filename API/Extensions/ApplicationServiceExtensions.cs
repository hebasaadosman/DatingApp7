
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>(); // Add the IUserRepository interface and the UserRepository class to the services container.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Add the AutoMapperProfiles class to the services container.
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings")); // Add the CloudinarySettings class to the services container.
            services.AddScoped<IPhotoService, PhotoService>(); // Add the IPhotoService interface and the PhotoService class to the services container.
            services.AddScoped<LogUserActivity>(); // Add the LogUserActivity class to the services container.
            services.AddScoped<ILikesRepository, LikesRepository>(); // Add the ILikesRepository interface and the LikesRepository class to the services container.
            services.AddScoped<IMessageRepository, MessageRepository>(); // Add the IMessageRepository interface and the MessageRepository class to the services container.
            services.AddSignalR(); // Add the SignalR service to the services container.
            services.AddSingleton<PresenceTracker>();//singleton because we want to keep the same instance of the presence tracker throughout the application
            return services;

        }

    }
}