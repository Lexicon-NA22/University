﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace University.Web.Filters
{
    public class NullRefferenseFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NullReferenceException)
            {
                context.Result = new StatusCodeResult(500);
            }
        }
    }
}
