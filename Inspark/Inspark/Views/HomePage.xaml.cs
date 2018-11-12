using Inspark.Models;
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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
            InitializeComponent();
		}

        public void NewsItemTapped(object sender, ItemTappedEventArgs e)
        {
            NewsPost selected = e.Item as NewsPost;
            var page = new NewsPostPage(selected);
            HomePageContent.Content = page.Content;
        }

        public void GroupItemTapped(object sender, ItemTappedEventArgs e)
        {
            GroupPost selected = e.Item as GroupPost;
            var page = new GroupPostPage(selected);
            HomePageContent.Content = page.Content;
        }

        public void ViewAllNewsTapped(object sender, EventArgs e)
        {
            var page = new NewsPage();
            HomePageContent.Content = page.Content;
        }

        public void ViewAllGroupTapped(object sender, EventArgs e)
        {
            var page = new GroupPage();
            HomePageContent.Content = page.Content;
        }
    }
}