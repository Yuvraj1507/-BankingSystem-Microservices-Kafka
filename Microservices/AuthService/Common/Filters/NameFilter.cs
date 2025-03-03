using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text.RegularExpressions;

namespace AuthService.Common.Filters
{
    public class NameFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var model = (RegistrationModel)context.ActionArguments.Single(x => x.Key == "model").Value;
            Regex rgx = new Regex("^[A-Z][a-z]*$");
            if (!rgx.IsMatch(model.FirstName) || !rgx.IsMatch(model.LastName))
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
