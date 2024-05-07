namespace PROWeb.Authentication.Components.Account.Configuration
{
    public static partial class Constants
    {
        public class Pages()
        {
            public const string Account = nameof(Account);

            public const string Login = $"{nameof(Account)}/{nameof(Login)}";

            public const string Register = $"{nameof(Account)}/{nameof(Register)}";

            public const string EmailConformation = $"{nameof(Account)}/{nameof(EmailConformation)}";

            public const string VerifyLoginCode = $"{nameof(Account)}/{nameof(VerifyLoginCode)}";

            public const string Lockout = $"{nameof(Account)}/{nameof(Lockout)}";

            public const string PasswordReset = $"{nameof(Account)}/{nameof(PasswordReset)}";

            public const string PasswordResetRequest = $"{nameof(Account)}/{nameof(PasswordResetRequest)}";
        }
    }
}
