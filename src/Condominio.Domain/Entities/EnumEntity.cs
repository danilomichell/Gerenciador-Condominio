using System.ComponentModel;

namespace Condominio.Domain.Entities;

public enum EnumCargo
{
    [Description("Usuario")] MORADOR = 1,
    [Description("Admin")] SINDICO = 2
}