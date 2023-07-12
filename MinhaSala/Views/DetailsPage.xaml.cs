using System;
using System.Collections.Generic;
using MinhaSala.Models;
using MinhaSala.ViewModels;
using Xamarin.Forms;

namespace MinhaSala.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(AirtableData data)
        {
            InitializeComponent();
            BindingContext = new DetailsPageViewModel(data);
        }
    }
}

