using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LuyenThiOnline.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
namespace LuyenThiOnline.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var repo = resultContext.HttpContext.RequestServices.GetService<IAuthRepository>();
                var user = await repo.GetUserTracking(userId);
                user.LastActive = DateTime.Now;
                await repo.SaveAll();
            }
        }
    }
}