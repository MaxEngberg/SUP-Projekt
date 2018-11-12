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
	public partial class NewsPostPage : ContentPage
	{
        PostViewModel model;
        NewsPost thepost;

        public NewsPostPage(NewsPost post)
        {
            InitializeComponent();
            thepost = post;
            model = new PostViewModel(post);
            Content.BindingContext = model;
        }

        private void EditPost_Clicked(object sender, EventArgs e)
        {
            NewsPostView.Content = new EditPostPage(thepost).Content;
        }
    }
}