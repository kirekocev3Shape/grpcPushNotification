﻿namespace MauiBlazorApp
{
    public partial class App : Application
    {
        // oidcxamarin101:/authenticated
        public const string CallbackUri = "oidcxamarin101";
        public static readonly string CallbackScheme = $"{CallbackUri}:/authenticated";
        public static readonly string SignoutCallbackScheme = $"{CallbackUri}:/signout-callback-oidc";

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}