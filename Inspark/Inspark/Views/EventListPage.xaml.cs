using System;
using System.Collections.Generic;
using Inspark.Models;
using Xamarin.Forms;

namespace Inspark.Views
{
    public partial class EventListPage : ContentPage
    {
        public EventListPage()
        {
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Event selected = e.Item as Event;
            var page = new EventPage(selected);
            InsparkEventList.Content = page.Content;
        }

        public void CreateEvent_Clicked(object sender, EventArgs e)
        {
            var page = new CreateEventPage();
            InsparkEventList.Content = page.Content;
        }

        public void DeleteEvent_Clicked(object sender, EventArgs e)
        {
            var page = new DeleteEventPage();
            InsparkEventList.Content = page.Content;
        }

        public void ChangeEvent_Clicked(object sender, EventArgs e)
        {
            var page = new ChangeEventPage();
            InsparkEventList.Content = page.Content;
        }
    }
}
