using _123vendas.Domain.Base.Interfaces;

namespace _123vendas.Domain.Base;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}