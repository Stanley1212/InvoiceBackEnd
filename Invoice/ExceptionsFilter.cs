using Invoice.Core;
using Invoice.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice
{
    public class ExceptionsFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AppException exception)
            {
                if (exception.Code == (int)MessageCode.ResourceNotFound)
                {
                    context.Result = new NotFoundObjectResult(new
                    {
                        Code = 1008,
                        context.Exception.Message
                    });
                    return;
                }

                context.Result = new BadRequestObjectResult(new
                {
                    exception.Code,
                    context.Exception.Message
                });
                return;
            }
            context.Result = new BadRequestObjectResult(new
            {
                Code = MessageCode.GeneralException,
                Message = context.Exception.InnerException is null ? context.Exception.Message : context.Exception.InnerException.Message
            });
        }

    }
}
