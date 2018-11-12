using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspark.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inspark;
using Inspark.Models;

namespace Inspark.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminPage : ContentPage
	{
		public AdminPage ()
		{
			InitializeComponent ();
        }

        public void CreateGroupButton_Clicked(object sender, EventArgs e)
        {
            var page = new CreateGroupPage();
            InsparkAdmin.Content = page.Content;
        }

        public void ChangeGroupButton_Clicked(object sender, EventArgs e)
        {
            var page = new ChangeGroupPage();
            InsparkAdmin.Content = page.Content;
        }

	    public void DeleteGroupButton_Clicked(object sender, EventArgs e)
	    {
	        var page = new DeleteGroupPage();
	        InsparkAdmin.Content = page.Content;
	    }

	    public void AddUserToGroupButton_Clicked(object sender, EventArgs e)
	    {
            var page = new AddUserToGroupPage();
	        InsparkAdmin.Content = page.Content;
	    }

	    public void DeleteUserFromGroupButton_Clicked(object sender, EventArgs e)
	    {
            var page = new DeleteUserFromGroupPage();
	        InsparkAdmin.Content = page.Content;
	    }

	    public void CreateNewsPostButton_Clicked(object sender, EventArgs e)
	    {
            var page = new CreateNewsPostPage();
	        InsparkAdmin.Content = page.Content;
	    }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            User selected = e.Item as User;
            var page = new ProfilePage(selected);
            InsparkAdmin.Content = page.Content;
        }
	}
}