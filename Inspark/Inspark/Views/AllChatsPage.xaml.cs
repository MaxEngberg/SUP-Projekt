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
	public partial class AllChatsPage : ContentPage
	{
        AllChatsViewModel vm;
		public AllChatsPage ()
		{
			InitializeComponent ();
            vm = new AllChatsViewModel();
            Content.BindingContext = vm;
            ChatListArea.BindingContext = vm;
            SearchArea.BindingContext = vm;
            GroupChatArea.BindingContext = vm;
        }

        void User_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            User selected = e.Item as User;
            vm.CreateChat(selected);
        }

        void Chat_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ChatDisplayModel selected = e.Item as ChatDisplayModel;
            vm.OpenChat(selected);
        }

        void GroupChat_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            GroupChatDisplayModel selected = e.Item as GroupChatDisplayModel;
            vm.OpenChat(selected);
        }

    }
}