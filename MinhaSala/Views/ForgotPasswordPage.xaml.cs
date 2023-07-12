using System;
using System.Collections.Generic;
using MinhaSala.ViewModels;
using Xamarin.Forms;

namespace MinhaSala.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
            BindingContext = new ForgotPasswordPageViewModel();
        }
    }
}

