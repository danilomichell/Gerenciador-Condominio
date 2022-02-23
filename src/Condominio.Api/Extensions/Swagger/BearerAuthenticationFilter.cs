using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Condominio.Api.Extensions.Swagger;

/// <summary>
///     Filtro para a ui do swagger exibir o ícone (e enviar o token) de autenticação nos endpoints necessários
/// </summary>
public class BearerAuthenticationFilter : IOperationFilter
{
    /// <summary>
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var allowAnon = context.ApiDescription.CustomAttributes()
            .Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute));
        var authorize = context.ApiDescription.CustomAttributes()
            .Any(attr => attr.GetType() == typeof(AuthorizeAttribute));

        if (allowAnon || !authorize) return;

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            }
        };
    }
}