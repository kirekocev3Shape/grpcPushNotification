using Grpc.Net.Client;
using MauiBlazorApp.Data;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MauiBlazorApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .RegisterBlazorMauiWebView()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddBlazorWebView();
            builder.Services.AddSingleton<WeatherForecastService>();

            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            builder.Services.AddSingleton(ser =>
            {
                try
                {
                    var httpClientHandler = new HttpClientHandler();
#if __ANDROID_11__
                    //httpClientHandler = new Xamarin.Android.Net.AndroidClientHandler();
#endif
                    httpClientHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true; //HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                    httpClientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;

                    //var backendUrl = "http://10.0.2.2:5002"; // android
                    var backendUrl = "https://10.0.2.2:7002"; // android
                    //var backendUrl = "http://localhost:5002"; //windows
                    //var backendUrl = "https://localhost:7002"; //windows

                    var baseUri = new Uri(backendUrl);
                    var options = new GrpcChannelOptions
                    { 
                        //HttpHandler = httpClientHandler,
                        HttpClient = new HttpClient(httpClientHandler)
                    };
                    //options.Credentials = Grpc.Core.ChannelCredentials.SecureSsl;
                    //GrpcSslContexts

                    var channel = GrpcChannel.ForAddress(baseUri, options);
                    return GrpcChannel.ForAddress(baseUri, options);
                }
                catch (Exception ex)
                {
                    var e = ex;
                }
                return null;
            });

            
            return builder.Build();
        }
    }
}