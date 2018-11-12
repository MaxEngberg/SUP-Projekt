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
	public partial class ChatPage : ContentPage
	{
        ChatViewModel vm;

		public ChatPage (Chat chat)
		{
            InitializeComponent();
            vm = new ChatViewModel(chat);
            Content.BindingContext = vm;
            MessageArea.BindingContext = vm;
            TextArea.BindingContext = vm;

            vm.Messages.CollectionChanged += (sender, e) =>
            {
                var target = vm.Messages[vm.Messages.Count - 1];
                MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            };
        }

        public ChatPage (GroupChat chat)
        {
            InitializeComponent();
            vm = new ChatViewModel(chat);
            Content.BindingContext = vm;
            MessageArea.BindingContext = vm;
            TextArea.BindingContext = vm;

            vm.Messages.CollectionChanged += (sender, e) =>
            {
                var target = vm.Messages[vm.Messages.Count - 1];
                MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            };
        }

        void MyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        void MyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }
    }
}