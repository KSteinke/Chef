using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef_API.Filters.ActionFilters
{
    public class UploadRecipeValidation:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            RecipeDtoWrapped recipeDtoWrapped = (RecipeDtoWrapped)context.ActionArguments["recipeDtoWrapped"];
            long maxFileSize = 1024 * 512 * 1; //TODO - specify in the appsettings

            if(recipeDtoWrapped.RecipeImg == null)
                context.ModelState.AddModelError("RecipeImg", "Image is required.");
            if(recipeDtoWrapped.RecipeImg.Length > maxFileSize)
                context.ModelState.AddModelError("RecipeImg", "Image file is to big.");
            if (recipeDtoWrapped.RecipeImg.ContentType != "image/jpeg")
                context.ModelState.AddModelError("RecipeImg", "Image must be uploaded in image/jpeg format.");
            if(recipeDtoWrapped.RecipeDtoJson.Length < 1)
                context.ModelState.AddModelError("RecipeDto", "RecipeDto in Json is required.");
            

            if (context.ModelState.Count > 0)
                context.Result = new BadRequestObjectResult(context.ModelState);

        }
    }
}
