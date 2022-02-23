using Condominio.Api.Filter;
using Condominio.Data.Persistence;
using Condominio.Data.Repositories;
using Condominio.Domain.Interfaces.Repositories;
using Condominio.Domain.Interfaces.Util;
using Condominio.Service.Services;
using Condominio.Service.Services.Interface;
using Condominio.Util.Cryptography;

namespace Condominio.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ApiExceptionFilterAttribute>();
        services.ResolveDependeciesRepository();
        services.ResolveDependeciesService();
        return services;
    }

    private static void ResolveDependeciesService(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITokenService, TokenService>();
    }

    private static void ResolveDependeciesRepository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWorkImdb, UnitOfWorkImdb>();

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ICryptograph, Sha256Cryptograph>();
    }
}