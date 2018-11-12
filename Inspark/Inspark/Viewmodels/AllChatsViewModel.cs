using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class AllChatsViewModel : BaseViewModel
    {
        private string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; OnPropertyChanged(); }
        }

        private bool _isSearchListVisible;

        public bool IsSearchListVisible
        {
            get { return _isSearchListVisible; }
            set { _isSearchListVisible = value; OnPropertyChanged(); }
        }

        private ObservableRangeCollection<GroupChat> _groupChats;

        public ObservableRangeCollection<GroupChat> GroupChats
        {
            get { return _groupChats; }
            set { _groupChats = value; OnPropertyChanged(); }
        }

        private ObservableRangeCollection<GroupChatDisplayModel> _groupChatDisplayModels;

        public ObservableRangeCollection<GroupChatDisplayModel> GroupChatDisplayModels
        {
            get { return _groupChatDisplayModels; }
            set { _groupChatDisplayModels = value; OnPropertyChanged(); }
        }



        private ObservableCollection<User> _allUsers;

        public ObservableCollection<User> AllUsers
        {
            get { return _allUsers; }
            set { _allUsers = value; OnPropertyChanged(); }
        }

        private ObservableRangeCollection<User> _suggestions;

        public ObservableRangeCollection<User> Suggestions
        {
            get { return _suggestions; }
            set { _suggestions = value; OnPropertyChanged(); }
        }


        private ObservableRangeCollection<Chat> _chats;

        public ObservableRangeCollection<Chat> Chats
        {
            get { return _chats; }
            set { _chats = value; OnPropertyChanged(); }
        }

        private ObservableRangeCollection<ChatDisplayModel> _chatDisplayModels;

        public ObservableRangeCollection<ChatDisplayModel> ChatDisplayModels
        {
            get { return _chatDisplayModels; }
            set { _chatDisplayModels = value; OnPropertyChanged(); }
        }

        private ObservableRangeCollection<string> _viewed;

        public ObservableRangeCollection<string> Viewed
        {
            get { return _viewed; }
            set { _viewed = value; OnPropertyChanged(); }
        }



        public User User { get; set; }

        public ICommand SearchCommand
        {
            get { return new Command(Search); }
        }

        private void Search()
        {
            if (_keyword.Length >= 1)
            {
                var suggestionsList = AllUsers.Where(c => c.FirstName.ToLower().Contains
                 (_keyword.ToLower()) || c.LastName.ToLower().Contains(_keyword.ToLower()));

                var suggestionListCollection = new ObservableRangeCollection<User>(suggestionsList);
                if (suggestionListCollection.Where(x => x.Id == User.Id).Count() != 0)
                {
                    suggestionListCollection.Remove(suggestionListCollection.Where(x => x.Id == User.Id).First());
                }
                Suggestions = suggestionListCollection;

                IsSearchListVisible = true;
            }
            else
            {
                IsSearchListVisible = false;
            }
        }

        public AllChatsViewModel()
        {
            OnLoad();
        }

        public async void OpenChat(ChatDisplayModel cdm)
        {
            var chat = Chats.Where(x => x.Id == cdm.Id).First();
            if (chat.Views != null && chat.Views.Count != 0)
            {
                var list = chat.Views.Where(x => x.UserId == User.Id);
                if (list.Count() != 0)
                {
                    var view = new Models.View
                    {
                        UserId = User.Id
                    };
                    await _api.AddViewToChat(view, chat.Id);
                }
            }
            Application.Current.MainPage = new MainPage(new ChatPage(chat));
        }

        public async void OpenChat(Chat chat)
        {
            if (chat.Views != null && chat.Views.Count != 0)
            {
                var list = chat.Views.Where(x => x.UserId == User.Id);
                if (list.Count() != 0)
                {
                    var view = new Models.View
                    {
                        UserId = User.Id
                    };
                    await _api.AddViewToChat(view, chat.Id);
                }
            }
            Application.Current.MainPage = new MainPage(new ChatPage(chat));
        }

        public async void OpenChat(GroupChat chat)
        {
            if(chat.Views != null && chat.Views.Count != 0)
            {
                var list = chat.Views.Where(x => x.UserId == User.Id);
                if (list.Count() != 0)
                {
                    var view = new Models.View
                    {
                        UserId = User.Id
                    };
                    await _api.AddViewToGroupChat(view, chat.Id);
                }
            }
            Application.Current.MainPage = new MainPage(new ChatPage(chat));
        }

        public async void OpenChat(GroupChatDisplayModel gcdm)
        {
            var chat = GroupChats.Where(x => x.Id == gcdm.Id).First();
            if (chat.Views != null)
            {
                var list = chat.Views.Where(x => x.UserId == User.Id);
                if (list.Count() != 0)
                {
                    var view = new Models.View
                    {
                        UserId = User.Id
                    };
                    await _api.AddViewToGroupChat(view, chat.Id);
                }
            }
            Application.Current.MainPage = new MainPage(new ChatPage(chat));
        }

        public async void CreateChat(User reciver)
        {
            bool foundExistingChat = false;
            if (Chats.Count != 0)
            {
                foreach (var chat in Chats)
                {
                    var chatMembers = chat.Users;
                    foreach (var member in chatMembers)
                    {
                        if (member.Id == reciver.Id)
                        {
                            OpenChat(chat);
                            foundExistingChat = true;
                            break;
                        }
                    }
                    if (foundExistingChat)
                    {
                        break;
                    }
                }
            }
            if (foundExistingChat == false)
            {
                if (await _api.CreatePrivateChat(User.Id, reciver.Id))
                {
                    Application.Current.MainPage = new MainPage(new AllChatsPage());
                }
            }
        }

        async void OnLoad()
        {
            ChatDisplayModels = new ObservableRangeCollection<ChatDisplayModel>();
            Chats = new ObservableRangeCollection<Chat>();
            GroupChats = new ObservableRangeCollection<GroupChat>();
            GroupChatDisplayModels = new ObservableRangeCollection<GroupChatDisplayModel>();
            User = await _api.GetLoggedInUser();
            AllUsers = await _api.GetAllUsers();
            if(User.Chats.Count != 0)
            {
                Chats.AddRange(User.Chats);
            }
            if(User.GroupChats.Count != 0)
            {
                //GroupChats.AddRange(User.GroupChats);
            }
            if(GroupChats.Count != 0)
            {
                foreach(var gc in GroupChats)
                {
                    bool IsMessagesNotViewed = true;
                    var gcdm = new GroupChatDisplayModel();
                    gcdm.Id = gc.Id;
                    if(gc.GroupName != null && gc.GroupName != "" && gc.GroupChatPic != null)
                    {
                        gcdm.ChatName = gc.GroupName;
                        gcdm.ChatPic = gc.GroupChatPic;
                    }
                    else
                    {
                        var assembly = Assembly.GetExecutingAssembly();
                        var pic = EmbeddedResourceToByteArray.GetEmbeddedResourceBytes(assembly, "group.png");
                        gcdm.ChatName = "GruppChatt";
                        gcdm.ChatPic = pic;
                    }

                    if(gc.Messages.Count() != 0 && gc.Messages != null)
                    {
                        gcdm.LatestMessage = gc.Messages.Last().Text;
                        gcdm.LatestMessageDate = gc.Messages.Last().MessageDateTime;
                        if (gc.Messages.Last().SenderId != User.Id)
                        {
                            IsMessagesNotViewed = false;
                        }
                        else
                        {
                            if (gc.Views.Count != 0 && gc.Views != null)
                            {
                                var list = gc.Views.Where(x => x.UserId == User.Id);
                                if (list.Count() != 0)
                                {
                                    IsMessagesNotViewed = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        gcdm.LatestMessage = "Inga meddelanden skickade";
                        IsMessagesNotViewed = false;
                    }
                    gcdm.IsLatestMessageNotViewed = IsMessagesNotViewed;
                    GroupChatDisplayModels.Add(gcdm);
                }
                GroupChatDisplayModels = new ObservableRangeCollection<GroupChatDisplayModel>(GroupChatDisplayModels.OrderByDescending(x => x.LatestMessageDate));
            }
            if(Chats.Count != 0)
            {
                foreach (var c in Chats)
                {
                    var otherUser = c.Users.Where(x => x.Id != User.Id).First();
                    bool IsMessagesNotViewed = true;
                    var chatView = new ChatDisplayModel
                    {
                        Id = c.Id,
                        DisplayName = otherUser.FirstName + " " + otherUser.LastName,
                        ChatPic = otherUser.ProfilePicture
                    };
                        
                    if (c.Messages.Count == 0)
                    {
                        chatView.LatestMessage = "Inga meddelanden skickade";
                        IsMessagesNotViewed = false;
                    }
                    else
                    {
                        
                        if (c.Messages.Last().SenderId == User.Id)
                        {
                            IsMessagesNotViewed = false;
                        }
                        else if(c.Views.Count != 0 && c.Views != null)
                        {
                            var list = c.Views.Where(x => x.UserId == User.Id);
                            if (list.Count() != 0)
                            {
                                IsMessagesNotViewed = false;
                            }
                        }
                        string latestMessageSender;
                        var latestMessageSenderId = c.Messages.Last().SenderId;
                        if(latestMessageSenderId == User.Id)
                        {
                            latestMessageSender = "Du: ";
                        }
                        else
                        {
                            latestMessageSender = otherUser.FirstName + ": ";
                        }
                        chatView.LatestMessage = latestMessageSender + c.Messages.Last().Text;
                        chatView.LatestMessageDate = c.Messages.Last().MessageDateTime;
                        chatView.IsLatestMessageNotViewed = IsMessagesNotViewed;
                    }
                    ChatDisplayModels.Add(chatView);
                }
                ChatDisplayModels = new ObservableRangeCollection<ChatDisplayModel>(ChatDisplayModels.OrderByDescending(x => x.LatestMessageDate));
            }
        }
    }
}
