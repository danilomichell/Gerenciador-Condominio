using System.Security.Claims;

namespace Condominio.Util.Extensions;

public static class ClaimsPrincipalExtension
{
    /// <summary>
    ///     Responsável por obter uma Claim pelo nome de seu tipo
    /// </summary>
    /// <param name="user">ClaimsPrincipal</param>
    /// <param name="typeName">Nome do tipo</param>
    /// <returns>A Claim pesquisada</returns>
    public static string? GetByClaimTypeName(this ClaimsPrincipal user, string typeName)
    {
        return user.Claims.FirstOrDefault(x => x.Type == typeName)?.Value;
    }

    /// <summary>
    ///     Responsável por obter o Id do usuário
    /// </summary>
    /// <param name="claimsPrincipal">ClaimsPrincipal</param>
    /// <returns>A matrícula do usuário</returns>
    public static int GetId(this ClaimsPrincipal claimsPrincipal)
    {
        return Convert.ToInt32(claimsPrincipal.GetByClaimTypeName("UserId"));
    }

    /// <summary>
    ///     Responsável por obter o cargo do usuário
    /// </summary>
    /// <param name="claimsPrincipal">ClaimsPrincipal</param>
    /// <returns>O Nível do usuário</returns>
    public static string? GetCargo(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetByClaimTypeName("Cargo");
    }
}