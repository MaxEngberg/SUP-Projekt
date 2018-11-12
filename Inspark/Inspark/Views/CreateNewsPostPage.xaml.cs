using Inspark.Viewmodels;
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
	public partial class CreateNewsPostPage : ContentPage
	{
		public CreateNewsPostPage ()
		{
			InitializeComponent ();
		}

        public void Abort_Clicked(object sender, EventArgs e)
        {
            CreatePostContent.Content = new AdminPage().Content;
        }
    }
}