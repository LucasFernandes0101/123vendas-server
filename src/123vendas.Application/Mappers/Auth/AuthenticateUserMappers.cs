﻿using _123vendas.Application.Commands.Auth;
using _123vendas.Application.DTOs.Auth;
using _123vendas.Application.Results.Auth;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Auth;

public static class AuthenticateUserMappers
{
    private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddProfile<AuthenticateUserProfile>()).CreateMapper();

    public static AuthenticateUserCommand ToCommand(this AuthenticateUserRequestDTO dto)
    {
        return _mapper.Map<AuthenticateUserCommand>(dto);
    }

    public static AuthenticateUserResult ToResult(this User dto)
    {
        return _mapper.Map<AuthenticateUserResult>(dto);
    }

    public static AuthenticateUserResponseDTO ToResponseDTO(this AuthenticateUserResult result)
    {
        return _mapper.Map<AuthenticateUserResponseDTO>(result);
    }
}