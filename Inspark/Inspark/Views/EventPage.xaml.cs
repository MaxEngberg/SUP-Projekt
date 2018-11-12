using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspark.Models;
using Inspark.Viewmodels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inspark.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventPage : ContentPage
	{
        EventViewModel model;

        public EventPage (Event e)
		{

            InitializeComponent();

            model = new EventViewModel(e);

            Content.BindingContext = model;
		}
	}
}