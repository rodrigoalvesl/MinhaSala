using System;
using System.Collections.Generic;
using MinhaSala.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace MinhaSala.Views
{
    public partial class CheckPopupPage : PopupPage
    {
        public CheckPopupPage(string id)
        {
            InitializeComponent();
            BindingContext = new PopupPageViewModel(id);
        }
    }
}

