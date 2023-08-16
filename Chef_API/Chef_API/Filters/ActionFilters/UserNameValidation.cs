using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef_API.Filters.ActionFilters
{
    public class UserNameValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            string userName = (string)context.ActionArguments["userName"];
            if(string.IsNullOrEmpty(userName) || userName.Length > 20)
            {
                context.ModelState.AddModelError("userName", "UserName must have length beetween 1 and 20");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
