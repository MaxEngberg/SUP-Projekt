using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inspark.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginInfoPage : ContentPage
	{
		public LoginInfoPage ()
		{
			InitializeComponent ();
		}

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}