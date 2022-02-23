using Condominio.Domain.Entities;

namespace Condominio.Service.Services.Interface;

public interface ITokenService
{
    Task<string> TokenGenerate(int id, string name, EnumCargo cargo);
}