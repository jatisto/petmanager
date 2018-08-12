using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace pet_manager.ActionFilters
{
    public class ModelValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context){
            if(!context.ModelState.IsValid){
                 context.Result = new ViewResult{
                    ViewData = ((Controller)context.Controller).ViewData
                };

                base.OnActionExecuting(context);
            }
        }
    }
}