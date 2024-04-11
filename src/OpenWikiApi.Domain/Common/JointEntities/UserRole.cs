using OpenWikiApi.Domain.Roles.ValueObjects;
using OpenWikiApi.Domain.Roles;
using OpenWikiApi.Domain.Users.ValueObjects;
using OpenWikiApi.Domain.Users;

namespace OpenWikiApi.Domain.Common.JointEntities;

public class UserRole
{
    public UserId UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public RoleId RoleId { get; set; } = null!;
    public Role Role { get; set; } = null!;
}