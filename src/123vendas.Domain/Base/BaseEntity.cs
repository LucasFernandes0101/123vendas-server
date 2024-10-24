using _123vendas.Domain.Base.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Domain.Base;

[ExcludeFromCodeCoverage]
public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}