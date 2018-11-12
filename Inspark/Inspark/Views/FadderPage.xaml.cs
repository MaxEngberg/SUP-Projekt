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
	public partial class FadderPage : ContentPage
	{
		public FadderPage ()
		{
			InitializeComponent ();
		}

        private void CreatePost_Clicked(object sender, EventArgs e)
        {
            FadderView.Content = new CreateGroupPostPage().Content;
        }

    }
}