using OpenWikiApi.Domain.Articles;
using OpenWikiApi.Domain.Common.JointEntities;
using OpenWikiApi.Domain.Common.Models;
using OpenWikiApi.Domain.Roles;
using OpenWikiApi.Domain.Users.Entities.UserCredentials;
using OpenWikiApi.Domain.Users.Entities.UserProfiles;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    private readonly List<Role> _roles = new();
    private readonly List<UserRole> _userRoles = new();

    public string Nickname { get; private set; }
    public UserCredential Credential { get; private set; }
    public UserProfile Profile { get; private set; }

    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();
    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();
    public IReadOnlyList<Article> Articles { get; private set; } = null!;

    private User(
        UserId id,
        string nickName,
        UserCredential credential,
        UserProfile profile
    ) : base(id)
    {
        Nickname = nickName;
        Credential = credential;
        Profile = profile;
    }

    public static User Create(
        string nickName,
        UserCredential credential,
        UserProfile profile
    )
    {
        return new(
            UserId.CreateUnique(),
            nickName,
            credential,
            profile
        );
    }

    public void UpdateUserCredential(UserCredential? updatedCredential)
    {
        Credential = updatedCredential!;
    }

    public void UpdateUserProfile(UserProfile? updatedProfile)
    {
        Profile = updatedProfile!;
    }

    public void AddRole(Role? role)
    {
        _roles.Add(role!);
    }

    public void AddRoleRange(List<Role>? roles)
    {
        _roles.AddRange(roles!);
    }

    public void AddUserRole(UserRole? userRole)
    {
        _userRoles.Add(userRole!);
    }

    public void AddUserRoleRange(List<UserRole>? userRole)
    {
        _userRoles.AddRange(userRole!);
    }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618
}