using ErrorOr;

namespace OpenWikiApi.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials =>
            Error.Unauthorized(
                code: "Authentication.InvalidCredentials",
                description: "Username and password are incorrect."
            );

        public static Error UsernameAlreadyInUse =>
            Error.Conflict(
                code: "Authentication.UsernameAlreadyInUse",
                description: "Username already exists."
            );

        public static Error InvalidUserIdentity =>
            Error.NotFound(
                code: "Authentication.InvalidUserIdentity",
                description: "User does not exist."
            );

        public static Error UserIsAlreadyAdmin =>
            Error.Conflict(
                code: "Authentication.UserIsAlreadyAdmin",
                description: "User is already an admin."
            );

        public static Error UserIsAlreadyMember =>
            Error.Conflict(
                code: "Authentication.UserIsAlreadyMember",
                description: "User is already a member."
            );
    }
}