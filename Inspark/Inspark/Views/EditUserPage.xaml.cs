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
	public partial class ChangeUserDetailsPage : ContentPage
	{
		public ChangeUserDetailsPage ()
		{
			InitializeComponent ();
		}

        private void Abort_Clicked(object sender, EventArgs e)
        {
            var page = new AccountPage();
            ChangeUserDetailsContent.Content = page.Content;
        }
	}
}