using Grpc.Net.Client;
using Grpc.Net.Client.Web;
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

            builder.Services.AddSingleton(ser =>
            {
                var backendUrl = "http://10.0.2.2:5002"; // android
                //var backendUrl = "https://10.0.2.2:7002"; // android
                //var backendUrl = "http://localhost:5002"; //windows
                //var backendUrl = "https://localhost:7002"; //windows

                var baseUri = new Uri(backendUrl);
                return GrpcChannel.ForAddress(baseUri);
            });

            return builder.Build();
        }
    }
}