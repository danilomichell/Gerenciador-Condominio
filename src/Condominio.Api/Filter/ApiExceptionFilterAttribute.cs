using System.Net;
using System.Text;
using System.Text.Json;
using Condominio.Util.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Condominio.Api.Filter;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<ApiExceptionFilterAttribute> _logger;

    public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotImplemented;
            context.HttpContext.Response.Headers.Clear();
            var mensagem = new StringBuilder();
            foreach (var validationsfailures in (context.Exception as ValidationException)!.Errors!)
                mensagem.Append($"- {validationsfailures.ErrorMessage}{Environment.NewLine}");
            context.Result = new ContentResult
            {
                Content = mensagem.ToString(),
                StatusCode = (int) HttpStatusCode.BadRequest,
                ContentType = "text/plain"
            };
            return;
        }

        if (context.Exception is InvalidOperationException)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            context.HttpContext.Response.Headers.Clear();
            context.Result = new ContentResult
            {
                Content = context.Exception.Message,
                StatusCode = (int) HttpStatusCode.NotFound,
                ContentType = "text/plain"
            };
            return;
        }

        if (context.Exception is ArgumentNullException or ArgumentOutOfRangeException or ArgumentException
            or OperationCanceledException)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.HttpContext.Response.Headers.Clear();
            context.Result = new ContentResult
            {
                Content = context.Exception.Message,
                StatusCode = (int) HttpStatusCode.BadRequest,
                ContentType = "text/plain"
            };
            return;
        }

        if (context.Exception is NotImplementedException)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotImplemented;
            context.HttpContext.Response.Headers.Clear();
            context.Result = new ContentResult
            {
                Content = "Funcionalidade ainda não implementada.",
                StatusCode = (int) HttpStatusCode.NotImplemented,
                ContentType = "text/plain"
            };
            return;
        }

        if (context.Exception is DbUpdateException)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotImplemented;
            context.HttpContext.Response.Headers.Clear();
            context.Result = new ContentResult
            {
                Content = context.Exception.GetAllMessagesAsString(),
                StatusCode = (int) HttpStatusCode.InternalServerError,
                ContentType = "text/plain"
            };
            return;
        }

        if (context.Exception is JsonException)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.HttpContext.Response.Headers.Clear();
            context.Result = new ContentResult
            {
                Content = context.Exception.Message,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                ContentType = "text/plain"
            };
            return;
        }

        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        context.HttpContext.Response.Headers.Clear();
        context.Result = new ContentResult
        {
            Content = context.Exception.Message,
            StatusCode = (int) HttpStatusCode.InternalServerError,
            ContentType = "text/plain"
        };

        _logger.LogError(context.Exception, context.Exception.Message);
    }
}