using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.RoleFeatures.Command.CreateRole;
public sealed record CreateRoleCommand(
    string Name) : IRequest<MessageResponse>;
