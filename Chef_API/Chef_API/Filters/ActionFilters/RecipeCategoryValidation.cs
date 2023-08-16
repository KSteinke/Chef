using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef_API.Filters.ActionFilters
{
    public class RecipeCategoryValidation:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
         {
            base.OnActionExecuting(context);

            string category = (string)context.ActionArguments["category"];

            if(category != "Breakfast"
                && category != "Dinner"
                && category != "Supper"
                && category != "Snack")
            {
                context.ModelState.AddModelError("Category", "Category must be one of those values: Breakfast, Dinner, Supper, Snack");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
