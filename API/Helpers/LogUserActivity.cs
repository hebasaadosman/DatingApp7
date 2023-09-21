

using System.Security.Claims;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        // We want to update the last active property of the user when they make a request to our API.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();// We want to execute the action and then we want to do something after the action has been executed.
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
            var userId = resultContext.HttpContext.User.GetUserId();

            var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();// We need to get an instance of our repository.
            var user = await repo.GetUserByIdAsync(int.Parse(userId.ToString()));
            user.LastActive = DateTime.UtcNow;
            await repo.SaveAllAsync();
        }
    }
}