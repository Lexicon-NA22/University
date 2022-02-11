using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace University.Web.Filters
{
    public class RequiredParam : ActionFilterAttribute
    {
        private readonly string param;

        public RequiredParam(string param)
        {
            this.param = param;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue(param, out object _))
                context.Result = new NotFoundResult();
        }
    }
}
