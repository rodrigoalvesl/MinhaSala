using System;
using System.Collections.Generic;
using MinhaSala.Views;
using Xamarin.Forms;

namespace MinhaSala
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("DetailsPage", typeof(DetailsPage));
            Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
            Routing.RegisterRoute("CheckPage", typeof(CheckPage));
            Routing.RegisterRoute("ForgotPage", typeof(ForgotPasswordPage));
            Routing.RegisterRoute("ChangePage", typeof(ChangeUserPage));

        }
    }
}

