using CleanArchitecture.Domain.abstractions;

namespace CleanArchitecture.Domain.Entities;
public sealed class UserRole : Entity
{
    //[ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }

    //[ForeignKey("Role")]
    public string RoleId { get; set; }
    public Role Role { get; set; }
}
