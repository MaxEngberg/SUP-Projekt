using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace Inspark.Viewmodels
{
    public class PostViewModel : BaseViewModel
    {
        public PostViewModel(NewsPost post)
        {
            Id = post.Id;
            Title = post.Title;
            Text = post.Text;
            Date = post.Date;
            Picture = post.Picture;
            SenderId = post.SenderId;
            Author = post.Author;
            SenderPic = post.SenderPic;
            SetDisplayDate();
            CheckUser(post);
            ViewsOnPost(post);
        }

        public PostViewModel(GroupPost post)
        {
            Id = post.Id;
            Title = post.Title;
            Text = post.Text;
            Date = post.Date;
            Picture = post.Picture;
            SenderId = post.SenderId;
            SenderPic = post.SenderPic;
            Author = post.Author;
            ViewsOnPost(post);
            GroupId = post.GroupId;
            SetDisplayDate();
            CheckUser(post);
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public byte[] Picture { get; set; }
        public string SenderId { get; set; }
        public string Author { get; set; }
        public byte[] SenderPic { get; set; }
        public int GroupId { get; set; }

        private string _displayDate;

        public string DisplayDate
        {
            get { return _displayDate; }
            set
            {
                if(_displayDate != value)
                {
                    _displayDate = value;
                    OnPropertyChanged();
                }           
            }
        }

        private string _views;

        public string Views
        {
            get { return _views; }
            set
            {
                _views = value;
                OnPropertyChanged();
            }
        }

        public async void ViewsOnPost(GroupPost post)
        {
            var username = Settings.UserName;
            var views = post.Views;
            var containsuser = views.Where(x => x.UserName == username);
            int viewsCount;
            if (containsuser.Count() == 0)
            {
                await _api.AddUserToGroupPostViews(post.Id, username);
                viewsCount = views.Count + 1;
            }
            else
            {
                viewsCount = views.Count;
            }
            var group = await _api.GetGroup(post.GroupId);
            int members = group.Users.Count;
            Views = "Visat av " + viewsCount.ToString() + " utav " + members.ToString();
        }

        public async void ViewsOnPost(NewsPost post)
        {
            var username = Settings.UserName;
            var views = post.Views;
            var containsuser = views.Where(x => x.UserName == username);
            int viewsCount;
            if(containsuser.Count() == 0)
            {
                await _api.AddUserToNewsPostViews(post.Id, username);
                viewsCount = views.Count + 1;
            }
            else
            {
                viewsCount = views.Count;
            }
            var users = await _api.GetAllUsers();
            int members = users.Count;
            Views = "Visat av " + viewsCount.ToString() + " utav " + members.ToString();
        }


        private bool _isUserPost;

        public bool IsUserPost
        {
            get { return _isUserPost; }
            set
            {
                _isUserPost = value;
                OnPropertyChanged();
            }
        }


            public async void CheckUser(NewsPost post)
        {
            var user = await _api.GetLoggedInUser();
            if(user.Id == post.SenderId)
            {
                IsUserPost = true;
            }
            else
            {
                IsUserPost = false;
            }
        }

        public async void CheckUser(GroupPost post)
        {
            var user = await _api.GetLoggedInUser();
            if (user.Id == post.SenderId)
            {
                IsUserPost = true;
            }
            else
            {
                IsUserPost = false;
            }
        }

        public void SetDisplayDate()
        {
            CultureInfo culture = new CultureInfo("sv-SE");
            var today = DateTime.Now;
            if (today.Year == Date.Year)
            {
                DisplayDate = Date.Day.ToString() + " " + Date.ToString("MMMM", culture);
                if (today.Month == Date.Month)
                {
                    DisplayDate = Date.Day.ToString() + " " + Date.ToString("MMMM", culture);
                    if (today.Date.AddDays(-1).Day == Date.Day)
                    {
                        DisplayDate = "Igår " + Date.ToString("HH:mm");
                    }
                    if (today.Day == Date.Day)
                    {
                        DisplayDate = "Idag " + Date.ToString("HH:mm");
                    }
                }
                else
                {
                    DisplayDate = Date.Day.ToString() + " " + Date.ToString("MMMM", culture);
                }
            }
            else
            {
                DisplayDate = Date.ToString(culture.DateTimeFormat.LongDatePattern, culture);
            }
        }
    }
}
