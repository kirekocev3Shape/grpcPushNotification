using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<HttpClient>();
            serviceCollection.AddBlazorWebView();
            serviceCollection.AddSingleton(ser =>
            {
                //var backendUrl = "http://10.0.2.2:5002"; // android
                //var backendUrl = "https://10.0.2.2:7002"; // android
                //var backendUrl = "http://localhost:5002"; //windows
                var backendUrl = "https://localhost:7002"; //windows

                var baseUri = new Uri(backendUrl);
                return GrpcChannel.ForAddress(baseUri);
            });

            Resources.Add("services", serviceCollection.BuildServiceProvider());

            InitializeComponent();
        }
    }
    public partial class Main { }
}
