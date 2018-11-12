using System;
using System.Collections.Generic;
using Inspark.Models;
using Inspark.Viewmodels;
using Xamarin.Forms;

namespace Inspark.Views
{
    public partial class GroupEventPage : ContentPage
    {

        GroupEventViewModel model;

        public GroupEventPage(GroupEvent e)
        {
            InitializeComponent();
            model = new GroupEventViewModel(e);
            Content.BindingContext = model;
        }
    }
}
