using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DocStore.Utility
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BaseController : Controller
    {
        public CurrentUser CurrentUser { get; set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var principal = context.HttpContext.User as ClaimsPrincipal;
                CurrentUser = new CurrentUser
                {
                    UserId = principal.FindAll("sub").FirstOrDefault()?.Value,
                    UserEmailId = principal.FindAll("email").FirstOrDefault()?.Value,
                    UserGender = Convert.ToInt16(principal.FindAll("gender").FirstOrDefault()?.Value)
                };
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
