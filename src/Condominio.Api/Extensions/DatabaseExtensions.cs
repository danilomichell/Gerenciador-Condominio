using Condominio.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Condominio.Api.Extensions;

/// <summary>
///     Configurações de banco de dados
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    ///     Injeção dos contextos de banco de dados
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = Environment.GetEnvironmentVariable("CONNECTION") ??
                         configuration.GetConnectionString("CondominioContext");
        Console.WriteLine(connection);
        services.AddDbContext<CondominioContext>(options =>
            options.UseNpgsql(connection));
        return services;
    }
}