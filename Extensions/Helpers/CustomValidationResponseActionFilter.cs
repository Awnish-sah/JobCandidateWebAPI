using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web.Http.Filters;
using Contract.Enum;
using System.Web.Http.Results;
using IActionFilter = Microsoft.AspNetCore.Mvc.Filters.IActionFilter;

namespace Extensions.Helpers
{
    public class CustomValidationResponseActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();

                foreach (var modelState in context.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                var responseObj= new DataResult
                {
                    Success = false,
                    Error = String.Join(Environment.NewLine, errors),
                    Status = StatusCodes.Status400BadRequest,
                    Message = String.Join(Environment.NewLine, errors)
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
