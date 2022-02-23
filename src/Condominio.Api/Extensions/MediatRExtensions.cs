using Condominio.Api.Extensions.MediatR;
using FluentValidation;
using MediatR;

namespace Condominio.Api.Extensions;

/// <summary>
///     Classe criada para realizar a Injeção de dependencia nos repositorios.
/// </summary>
/// <returns></returns>
public static class MediatRExtensions
{
    /// <summary>
    ///     Construtor
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        var assembly = AppDomain.CurrentDomain.Load("Imdb.Service");
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}