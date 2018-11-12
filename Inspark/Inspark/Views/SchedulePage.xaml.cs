using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamForms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Inspark.Viewmodels;
using Inspark.Models;

namespace Inspark.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
      
        public SchedulePage()
        {
			InitializeComponent();
            Cal.StartDay = DayOfWeek.Monday;
            Cal.StartDate = DateTime.Now;
            Cal.MinDate = DateTime.Now.AddDays(-1);
  

        }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        
    }
}