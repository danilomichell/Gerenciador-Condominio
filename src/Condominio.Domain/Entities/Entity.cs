namespace Condominio.Domain.Entities;

public abstract class Entity
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}