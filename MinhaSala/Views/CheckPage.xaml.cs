using System;
using System.Collections.Generic;
using MinhaSala.ViewModels;
using Xamarin.Forms;

namespace MinhaSala.Views
{
    public partial class CheckPage : ContentPage
    {
        public CheckPage()
        {
            InitializeComponent();
            BindingContext = new CheckPageViewModel();
        }
    }
}

