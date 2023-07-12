using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MinhaSala.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MinhaSala
{
    public partial class App : Application
    {
        public static string WebAPIKey = "AIzaSyA9Sr5gaysDBThkcGZuOwgui9UXq8OUM4Q";
        public static string Username { get; set; }
        public static string Token { get; set; }


        public App ()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart ()
        {
            var appcenterItens = string.Join("", new[]{
                "android=34261e56-169f-4131-ac0f-0e7c3686da53;",
                "ios=2f050bd7-f215-4d9e-9c84-21cbac25d2bf;"
            });

            AppCenter.Start(appcenterItens, typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

