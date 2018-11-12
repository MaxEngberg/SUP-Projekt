using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using MvvmHelpers;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace Inspark.Viewmodels
{
    public class ChatViewModel : BaseViewModel
    {
        private ObservableRangeCollection<Message> _messages = new ObservableRangeCollection<Message>();

        public ObservableRangeCollection<Message> Messages
        {
            get { return _messages; }
        }

        private string _outgoingText = string.Empty;

        public string OutgoingText
        {
            get { return _outgoingText; }
            set
            {
                _outgoingText = value;
                OnPropertyChanged();
            }
        }

        private string _chatName;

        public string ChatName
        {
            get { return _chatName; }
            set { _chatName = value; OnPropertyChanged(); }
        }


        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public Chat Chat { get; set; }
        public GroupChat GroupChat { get; set; }
        public int ChatId { get; set; }
        public bool IsGroupChat { get; set; }

        public User User { get; set; }

        public ICommand SendCommand => new Command(async() =>
        {
            var message = new Message
            {
                Text = OutgoingText,
                IsIncoming = false,
                MessageDateTime = DateTime.Now,
                SenderPic = User.ProfilePicture,
                SenderId = User.Id,
            };
            _messages.Add(message);
            if (IsGroupChat)
            {
                if(await _api.PostGroupMessage(ChatId, message))
                {
                    if(await _api.ClearGroupViews(ChatId))
                    {
                        var view = new Models.View
                        {
                            UserId = User.Id
                        };
                        await _api.AddViewToGroupChat(view, ChatId);
                    }
                }
            } else
            {
                if (await _api.PostPrivateMessage(ChatId, message))
                {
                    if (await _api.ClearViews(ChatId))
                    {
                        var view = new Models.View
                        {
                            UserId = User.Id
                        };
                        await _api.AddViewToChat(view, Chat.Id);
                    }
                }
            }
           
            OutgoingText = string.Empty;
        });

        public ChatViewModel(Chat chat)
        {
            OnLoad(chat);
        }

        public ChatViewModel(GroupChat chat)
        {
            OnLoad(chat);
        }

        async void OnLoad(GroupChat chat)
        {
            IsGroupChat = true;
            GroupChat = chat;
            ChatId = chat.Id;
            User = await _api.GetLoggedInUser();
            ChatName = chat.GroupName;
            var SortedMessages = new ObservableCollection<Message>();
            if (chat.Messages.Count != 0)
            {
                Messages.AddRange(chat.Messages);
                foreach (var message in Messages)
                {
                    if (message.SenderId != User.Id)
                    {
                        message.IsIncoming = true;
                    }

                    SortedMessages.Add(message);
                }
                Messages.ReplaceRange(SortedMessages);
            }

        }

        async void OnLoad(Chat chat)
        {
            IsGroupChat = false;
            ChatId = chat.Id;
            Chat = chat;
            User = await _api.GetLoggedInUser();
            var otherUser = chat.Users.Where(x => x.Id != User.Id).First();
            ChatName = otherUser.FirstName + " " + otherUser.LastName;
            var SortedMessages = new ObservableCollection<Message>();
            if (chat.Messages.Count != 0)
            {
                Messages.AddRange(chat.Messages);
                foreach (var message in Messages)
                {
                    if (message.SenderId != User.Id)
                    {
                        message.IsIncoming = true;
                    }
                    SortedMessages.Add(message);
                }
                Messages.ReplaceRange(SortedMessages);
            }
        }
    }
}
