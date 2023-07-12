using System;
using System.Collections.Generic;
using MinhaSala.ViewModels;
using Xamarin.Forms;

namespace MinhaSala.Views
{
    //[QueryProperty(nameof(Registration), "number")]
    public partial class SignUpPage : ContentPage
    {
        //SignUpPageViewModel vm;
        //public string Registration
        //{
        //    set
        //    {
        //        Test(value);
        //    }
        //}
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpPageViewModel();
        }

        void Test(string name)
        {
            Console.WriteLine($"======={name}");
            //BindingContext = vm = new SignUpPageViewModel();

        }
    }
}

