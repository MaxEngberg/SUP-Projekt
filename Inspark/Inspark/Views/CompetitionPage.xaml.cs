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
	public partial class CompetitionPage : ContentPage
	{
		public CompetitionPage ()
		{
			InitializeComponent ();
		}

        public void AddScore_Clicked(object sender, EventArgs e)
        {
            var page = new AddScorePage();
            InsparkCompetition.Content = page.Content;
        }

        public void DeleteScore_Clicked(object sender, EventArgs e)
        {
            var page = new DeleteScorePage();
            InsparkCompetition.Content = page.Content;
        }

    }
}