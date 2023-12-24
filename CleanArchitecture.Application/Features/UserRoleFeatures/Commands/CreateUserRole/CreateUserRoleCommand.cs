using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole;
public sealed record CreateUserRoleCommand(
    string UserId,
    string RoleId) : IRequest<MessageResponse>;
