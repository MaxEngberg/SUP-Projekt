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
	public partial class NewsPage : ContentPage
	{
		public NewsPage ()
		{
			InitializeComponent();
		}

        public void PostTapped(object sender, ItemTappedEventArgs e)
        {
            NewsPost selected = e.Item as NewsPost;
            var page = new NewsPostPage(selected);
            News.Content = page.Content;
        }

    }
}