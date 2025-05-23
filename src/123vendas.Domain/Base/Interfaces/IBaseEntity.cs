﻿namespace _123vendas.Domain.Base.Interfaces;

public interface IBaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}