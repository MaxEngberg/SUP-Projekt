using Inspark.Models;
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
	public partial class GroupPostsPage : ContentPage
	{
		public GroupPostsPage ()
		{
			InitializeComponent ();
		}

        public void PostTapped(object sender, ItemTappedEventArgs e)
        {
            GroupPost selected = e.Item as GroupPost;
            var page = new GroupPostPage(selected);
            GroupPostsView.Content = page.Content;
        }
    }
}