using Android.App;
using Android.Content.PM;
using Android.Content;

namespace MauiBlazorApp.Platforms.Android
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = App.CallbackUri)]
    public class WebAuthenticationCallbackActivity : Microsoft.Maui.Essentials.WebAuthenticatorCallbackActivity
    {
    }
}
