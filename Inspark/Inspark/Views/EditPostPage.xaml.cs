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
	public partial class EditPostPage : ContentPage
	{
        EditPostViewModel model;
		public EditPostPage (NewsPost post)
		{
			InitializeComponent ();
            model = new EditPostViewModel(post);
            Content.BindingContext = model;
		}

        public EditPostPage(GroupPost post)
        {
            InitializeComponent();
            model = new EditPostViewModel(post);
            Content.BindingContext = model;
        }

        private void Abort_Clicked(object sender, EventArgs e)
        {

        }
    }
}