using Humanizer;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Inspark.Models
{
    public class Message : ObservableObject
    {
        public int MessageID { get; set; }

        string text;

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        DateTime messageDateTime;

        public DateTime MessageDateTime
        {
            get { return messageDateTime; }
            set { SetProperty(ref messageDateTime, value); }
        }

        bool isIncoming;

        public bool IsIncoming
        {
            get { return isIncoming; }
            set { SetProperty(ref isIncoming, value); }
        }

        byte[] senderPic;

        public byte[] SenderPic
        {
            get { return senderPic; }
            set { SetProperty(ref senderPic, value); }
        }

        string senderId;

        public string SenderId
        {
            get { return senderId; }
            set { SetProperty(ref senderId, value); }
        }
    }
}
