﻿using CleanArchitecture.Application.Features.RoleFeatures.Command.CreateRole;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services;
public sealed class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task CreateAsync(CreateRoleCommand request)
    {
        Role role = new()
        {
            Name = request.Name
        };

        await _roleManager.CreateAsync(role);
    }
}