using System.ComponentModel;
using System.Reflection;

namespace Condominio.Util.Extensions;

public static class EnumExtesions
{
    #region GetDescription

    /// <summary>
    ///     Retorna a descrição do ENUM
    /// </summary>
    /// <param name="value">Enum</param>
    /// <returns>Enum Description</returns>
    public static string GetDescription(this Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description ?? value.ToString();
    }

    #endregion
}