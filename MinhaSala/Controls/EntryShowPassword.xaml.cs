using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MinhaSala.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryShowPassword : ContentView
    {
        public static readonly BindableProperty PlaceholderProperty =
             BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryShowPassword));

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryShowPassword),
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty HidePasswordProperty =
            BindableProperty.Create(nameof(HidePassword), typeof(bool), typeof(EntryShowPassword),
                defaultValue: true);

        public static readonly BindableProperty HidePasswordColorProperty =
            BindableProperty.Create(nameof(HidePasswordColor), typeof(Color), typeof(EntryShowPassword),
                defaultValue: Color.Black);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool HidePassword
        {
            get => (bool)GetValue(HidePasswordProperty);
            set => SetValue(HidePasswordProperty, value);
        }

        public Color HidePasswordColor
        {
            get => (Color)GetValue(HidePasswordColorProperty);
            set => SetValue(HidePasswordColorProperty, value);
        }

        public EntryShowPassword()
        {
            InitializeComponent();
        }

        private void OnImageButtonClicked(object sender, EventArgs e)
        {
            HidePassword = !HidePassword;
        }

        void entry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(entry.Text))
                imButton.IsVisible = false;
            else
                imButton.IsVisible = true;
        }
    }


}

