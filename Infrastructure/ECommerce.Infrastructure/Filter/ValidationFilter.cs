using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Filter
{
    public class ValidationFilter : IAsyncActionFilter
    {   
        // Action'a gelen isteklerde devreye giren bir filter validasyon operasyonlarını gerçekleştirecez.
        // Bunu yapmamızın sebebi default olarak gelen filter'ı program.cs de kapattık.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors =context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(error => error.Key, error => error.Value.Errors.Select(error => error.ErrorMessage))
                    .ToArray();

                context.Result = new BadRequestObjectResult(errors);
                return; // return demessek durumu bildirmeden diğerine geçer.
            }

            await next(); // ilerleyip diğerlerine bakacak.
        }
    }
}
