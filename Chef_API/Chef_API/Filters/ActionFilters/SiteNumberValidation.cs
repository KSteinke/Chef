using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef_API.Filters.ActionFilters
{
    public class SiteNumberValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            int siteNumber = (int)context.ActionArguments["siteNumber"];

            if(siteNumber < 0)
            {
                context.ModelState.AddModelError("siteNumber", "SiteNumber can't be lower than 0.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
