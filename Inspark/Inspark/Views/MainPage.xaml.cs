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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(ContentPage page)
        {
            InitializeComponent();
            Inspark.Content = page.Content;
        }

        public void HomeIcon_Tapped(object sender, EventArgs e)
        {
            var page = new HomePage();
            Inspark.Content = page.Content;
        }

        public void GroupIcon_Tapped(object sender, EventArgs e)
        {
            var page = new GroupPage();
            Inspark.Content = page.Content;
        }

        public void EventIcon_Tapped(object sender, EventArgs e)
        {
            var page = new EventListPage();
            Inspark.Content = page.Content;
        }

        public void ChatIcon_Tapped(object sender, EventArgs e)
        {
            var page = new AllChatsPage();
            Inspark.Content = page.Content;
        }

        public void ScheduleIcon_Tapped(object sender, EventArgs e)
        {
            var page = new SchedulePage();
            Inspark.Content = page.Content;
        }

        public void ProfileIcon_Tapped(object sender, EventArgs e)
        {
            var page = new AccountPage();
            Inspark.Content = page.Content;
        }

        public void AdminIcon_Tapped(object sender, EventArgs e)
        {
            var page = new AdminPage();
            Inspark.Content = page.Content;
        }

        public void InfoIcon_Tapped(object sender, EventArgs e)
        {
            var page = new InfoPage();
            Inspark.Content = page.Content;
        }

        public void CompetitionIcon_Tapped(object sender, EventArgs e)
        {
            var page = new CompetitionPage();
            Inspark.Content = page.Content;
        }
    }
}