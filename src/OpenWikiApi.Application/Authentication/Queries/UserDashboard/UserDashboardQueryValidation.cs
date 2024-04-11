using FluentValidation;

namespace OpenWikiApi.Application.Authentication.Queries.UserDashboard;

public class UserDashboardQueryValidation : AbstractValidator<UserDashboardQuery>
{
    public UserDashboardQueryValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();
    }
}