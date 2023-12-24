using CleanArchitecture.Application.Features.RoleFeatures.Command.CreateRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services;
public interface IRoleService
{
    Task CreateAsync(CreateRoleCommand request);
}
