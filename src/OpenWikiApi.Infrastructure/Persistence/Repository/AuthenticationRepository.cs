using Microsoft.EntityFrameworkCore;

using OpenWikiApi.Application.Common.Interfaces.Persistence.Repository;
using OpenWikiApi.Domain.Roles;
using OpenWikiApi.Domain.Users;
using OpenWikiApi.Domain.Users.ValueObjects;
using OpenWikiApi.Infrastructure.Persistence.Context;

namespace OpenWikiApi.Infrastructure.Persistence.Repository;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly OpenWikiApiDbContext _context;

    public AuthenticationRepository(
        OpenWikiApiDbContext context
    )
    {
        _context = context;
    }

    public async Task<Role?> GetAdminRolesWithPermissions()
    {
        return await _context.Roles
            .Where(w => w.Name == Domain.Common.Constants.Role.Admin.ToString())
            .Include(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
            .FirstOrDefaultAsync();
    }

    public async Task<Role?> GetMemberRolesWithPermissions()
    {
        return await _context.Roles
            .Where(w => w.Name == Domain.Common.Constants.Role.Member.ToString())
            .Include(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
            .FirstOrDefaultAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByCredentialsAsync(string username, string password)
    {
        var user = await _context.Users
            .Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role)
                    .ThenInclude(rp => rp.RolePermissions)
                        .ThenInclude(p => p.Permission)
            .FirstOrDefaultAsync(u => u.Credential.Username == username && u.Credential.Password == password);

        return user;
    }

    public async Task<User?> GetUserById(UserId id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> CheckUsernameAsync(string username)
    {
        return await _context.Users
            .AnyAsync(u => u.Credential.Username == username);
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByIdWithRolesAndPermissions(UserId userId)
    {
        var user = await _context.Users
            .Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role)
                    .ThenInclude(rp => rp.RolePermissions)
                        .ThenInclude(p => p.Permission)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user;
    }

    public async Task<User?> GetUserByIdWithRolesAndArtilcles(UserId userId)
    {
        var user = await _context.Users
            .Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role)
            .Include(a => a.Articles)
                .ThenInclude(au => au.Updates)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user;
    }
}