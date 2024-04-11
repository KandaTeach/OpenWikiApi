using OpenWikiApi.Domain.Roles;
using OpenWikiApi.Domain.Users;
using OpenWikiApi.Domain.Users.ValueObjects;

namespace OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;

public interface IAuthenticationRepository
{
    Task<Role?> GetAdminRolesWithPermissions();
    Task<Role?> GetMemberRolesWithPermissions();
    Task<User?> GetUserByCredentialsAsync(string username, string password);
    Task<bool> CheckUsernameAsync(string username);
    Task<User?> GetUserById(UserId userId);
    Task<User?> GetUserByIdWithRolesAndPermissions(UserId userId);
    Task<User?> GetUserByIdWithRolesAndArtilcles(UserId userId);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
}