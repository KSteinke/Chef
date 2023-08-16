using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef_API.Filters.ActionFilters
{
    public class RecipeDietCategoryValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            string dietCategory = (string)context.ActionArguments["dietCategory"];

            if (dietCategory != "Natural"
                && dietCategory != "Vegetarian"
                && dietCategory != "Vegan")
            {
                context.ModelState.AddModelError("DietCategory", "Diet category must be one of those values: Natural, Vegetarian, Vegan");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
