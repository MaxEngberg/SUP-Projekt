using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Inspark
{
    public partial class RegisterConfirmEmailPage : ContentPage
    {
        public RegisterConfirmEmailPage()
        {
            InitializeComponent();
        }

        public async void Confirm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.MainPage());
        }
    }
}
