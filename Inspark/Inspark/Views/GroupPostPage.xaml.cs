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
	public partial class GroupPostPage : ContentPage
	{
        PostViewModel model;
        GroupPost thepost;

        public GroupPostPage(GroupPost post)
        {
            InitializeComponent();
            model = new PostViewModel(post);
            thepost = post;
            Content.BindingContext = model;
        }

        private void EditPost_Clicked(object sender, EventArgs e)
        {
            GroupPostView.Content = new EditPostPage(thepost).Content;
        }
    }
}