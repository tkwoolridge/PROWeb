using System.IdentityModel.Tokens.Jwt;

namespace PROWeb.WebSiteService.Authentication.Extensions
{
    public static class TokenExtensions
    {
        public static JwtSecurityToken DeserializeToken(this string strToken)
        {
            var token = new JwtSecurityToken(strToken);

            return token;
        }
    }
}
