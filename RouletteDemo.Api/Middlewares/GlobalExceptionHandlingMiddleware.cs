using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RouletteDemo.Api.Models;

namespace RouletteDemo.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(
            ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;
            
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next) 
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var error = new ServiceResponse<object>()
                {
                    Success = false,
                    Message = "An Internal server error has occured." + Environment.NewLine + exception.Message
                };

                if (exception is RequestException) 
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    error.Message = exception.Message;
                }

                httpContext.Response.ContentType = "application/json";

                var errorJson = JsonSerializer.Serialize(error);
                await httpContext.Response.WriteAsync(errorJson);
            }
        }
    }
}