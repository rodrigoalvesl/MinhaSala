using System;
using System.Collections.Generic;
using MinhaSala.ViewModels;
using Xamarin.Forms;

namespace MinhaSala.Views
{	
	public partial class ChangeUserPage : ContentPage
	{	
		public ChangeUserPage ()
		{
			InitializeComponent ();
			BindingContext = new ChangeUserPageViewModel();
		}
	}
}

