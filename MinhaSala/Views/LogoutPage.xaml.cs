using System;
using System.Collections.Generic;
using MinhaSala.ViewModels;
using Xamarin.Forms;

namespace MinhaSala.Views
{
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
            BindingContext = new LogoutPageViewModel();
        }
        protected override bool OnBackButtonPressed() => true;
    }
}

