using Microsoft.AspNetCore.Components;
using MauiBlazorApp.Services;
using System.Net.Http.Headers;
using System.Net;
using System.Diagnostics;

namespace MauiBlazorApp.Pages
{
    public class AuthOBase: ComponentBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        // username: testOAuth@oauth.com
        // password: testOAuth0oauth.com
        private const string AuthorityUrl = "https://xamarinoidc-app.azurewebsites.net";
                
        protected Credentials? _credentials;
        private readonly OidcIdentityService _oidcIdentityService;

        public AuthOBase()
        {
            _oidcIdentityService = new OidcIdentityService(
                "gnabbermobileclient", 
                App.CallbackScheme, 
                App.SignoutCallbackScheme, 
                "openid profile offline_access", 
                AuthorityUrl);
        }

        public bool IsLoggedIn => _credentials != null;

        protected async void Login()
        {
            Credentials credentials = await _oidcIdentityService.Authenticate();
            UpdateCredentials(credentials);

            _httpClient.DefaultRequestHeaders.Authorization = credentials.IsError
                ? null
                : new AuthenticationHeaderValue("bearer", credentials.AccessToken);
        }

        protected async void RefreshTokens()
        {
            if (_credentials?.RefreshToken == null) return;
            Credentials credentials = await _oidcIdentityService.RefreshToken(_credentials.RefreshToken);
            UpdateCredentials(credentials);
        }

        protected async void Logout()
        {
            await _oidcIdentityService.Logout(_credentials?.IdentityToken);
            UpdateCredentials(null);
        }

        protected void UpdateCredentials(Credentials credentials)
        {
            _credentials = credentials;
            StateHasChanged();
        }
    }
}
