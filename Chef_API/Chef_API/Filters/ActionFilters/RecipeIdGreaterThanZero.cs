using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef_API.Filters.ActionFilters
{
    public class RecipeIdGreaterThanZero : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            int value = (int)context.ActionArguments["recipeId"];
            if (value < 1)
            {
                context.ModelState.AddModelError("recipeId", "Id should be greater than 0");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
