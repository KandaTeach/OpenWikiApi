using Mapster;

using OpenWikiApi.Application.Authentication.Common;
using OpenWikiApi.Contract.Authentication;

namespace OpenWikiApi.Api.Common.Mappings;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, LoginResponse>()
            .Map(des => des, src => src.User)
            .Map(des => des.Name, src => src.User.Profile.Name)
            .Map(des => des.Age, src => src.User.Profile.Age)
            .Map(des => des.Email, src => src.User.Profile.Email)
            .Map(des => des.Token, src => src.Token);

        config.NewConfig<AuthenticationResult, RegisterResponse>()
            .Map(des => des, src => src.User)
            .Map(des => des.Name, src => src.User.Profile.Name)
            .Map(des => des.Age, src => src.User.Profile.Age)
            .Map(des => des.Email, src => src.User.Profile.Email);

        config.NewConfig<AuthenticationResult, UserDashboardResponse>()
            .Map(des => des.User, src => new UserInformationResponse(
                src.User.Id,
                src.User.Nickname,
                new UserProfileResponse(
                    src.User.Profile.Name,
                    src.User.Profile.Age,
                    src.User.Profile.Email
                ),
                new UserCredentialResponse(
                    src.User.Credential.Username
                ),
                src.User.Roles
                    .Select(
                        x => x.Name
                    ).ToList()
            ))
            .Map(des => des.Articles, src => src.User.Articles
                .ToDictionary(
                    article => article.Id,
                    article => article.Updates.Any() ? article.Updates
                        .OrderByDescending(
                            x => x.CreatedDateTime
                        )
                        .FirstOrDefault()!.Title
                    : article.Title
                )
            );
    }
}