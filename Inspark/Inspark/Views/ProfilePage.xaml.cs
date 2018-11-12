using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspark.Models;
using Inspark.Services;
using Inspark.Viewmodels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inspark.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
	    private ProfileViewModel model;

	    public ProfilePage()
	    {
			model = new ProfileViewModel();
            InitializeComponent();
			Content.BindingContext = model;
        }
        

		public ProfilePage (User e)
		{
			model = new ProfileViewModel(e);         
			InitializeComponent();
		    Content.BindingContext = model;
        }

		void AddCode_Clicked(object sender, EventArgs e)
		{
			var page = new IntroCodePage();
			Profile.Content = page.Content;
		}
	}
}