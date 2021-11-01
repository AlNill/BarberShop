using System;
using BarberShop.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BarberShop.MVC.Filters
{
    public class ExceptionFilterAttribute: Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            string message = $"In method {actionName} invoked exception: \n {exceptionMessage} \n {exceptionStack}";

            var logger = context.HttpContext.RequestServices.GetService<ILoggerService>();
            logger.LogError(message);
            context.ExceptionHandled = true;
            context.HttpContext.Response.Redirect("/About");
        }
    }
}
