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
	public partial class CreateGroupPostPage : ContentPage
	{
		public CreateGroupPostPage ()
		{
			InitializeComponent ();
		}

        private void Abort_Clicked(object sender, EventArgs e)
        {
            CreateGroupPostView.Content = new GroupPage().Content;
        }
	}
}