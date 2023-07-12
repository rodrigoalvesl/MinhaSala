using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaSala.ViewModels;
using Xamarin.Forms;

namespace MinhaSala.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();
        }
        protected override bool OnBackButtonPressed() => true;
    }
}

