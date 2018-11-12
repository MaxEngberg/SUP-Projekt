using System;
using System.Collections.Generic;
using Inspark.Models;
using Xamarin.Forms;

namespace Inspark.Views
{
    public partial class GroupEventListPage : ContentPage
    {
        public GroupEventListPage()
        {
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            GroupEvent selected = e.Item as GroupEvent;
            var page = new GroupEventPage(selected);
            GroupEvent.Content = page.Content;
        }

        void CreateGroupEvent_Clicked(object sender, EventArgs e)
        {
            GroupEvent.Content = new CreateGroupEventPage().Content;
        }

        public void DeleteGroupEvent_Clicked(object sender, EventArgs e)
        {
            var page = new DeleteGroupEventPage();
            GroupEvent.Content = page.Content;
        }

        public void ChangeGroupEvent_Clicked(object sender, EventArgs e)
        {
            var page = new ChangeGroupEventPage();
            GroupEvent.Content = page.Content;
        }
    }
}
