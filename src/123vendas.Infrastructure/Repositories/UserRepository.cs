﻿using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetActiveByEmailAsync(string email)
        => await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email &&
                                                           u.Status == UserStatus.Active);
}