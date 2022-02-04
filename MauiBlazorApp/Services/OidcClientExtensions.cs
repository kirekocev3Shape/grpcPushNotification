using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;

namespace MauiBlazorApp.Services
{
    public static class OidcClientExtensions
    {
        public static Credentials ToCredentials(this LoginResult loginResult)
        {   
            return new Credentials
            {
                AccessToken = loginResult.AccessToken,
                IdentityToken = loginResult.IdentityToken,
                RefreshToken = loginResult.RefreshToken,
                AccessTokenExpiration = loginResult.AccessTokenExpiration,
                Claims = loginResult.User.Claims
            };
        }

        public static Credentials ToCredentials(this RefreshTokenResult refreshTokenResult)
        {
            return new Credentials
            {
                AccessToken = refreshTokenResult.AccessToken,
                IdentityToken = refreshTokenResult.IdentityToken,
                RefreshToken = refreshTokenResult.RefreshToken,
                AccessTokenExpiration = refreshTokenResult.AccessTokenExpiration
            };
        }
    }
}
