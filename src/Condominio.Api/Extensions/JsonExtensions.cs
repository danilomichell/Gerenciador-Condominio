using System.Text.Json.Serialization;

namespace Condominio.Api.Extensions;

public static class JsonExtensions
{
    /// <summary>
    ///     Configurações de serialização json
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddCustomJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return builder;
    }
}