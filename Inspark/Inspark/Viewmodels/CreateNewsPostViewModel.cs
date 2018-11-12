using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class CreateNewsPostViewModel : BaseViewModel
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if(_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private byte[] _postImage;

        public byte[] PostImage
        {
            get { return _postImage; }
            set
            {
                if (_postImage != value)
                {
                    _postImage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _postTitle;

        public string PostTitle
        {
            get { return _postTitle; }
            set
            {
                if (_postTitle != value)
                {
                    _postTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _postText;

        public string PostText
        {
            get { return _postText; }
            set
            {
                if (_postText != value)
                {
                    _postText = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isPinned;

        public bool IsPinned
        {
            get { return _isPinned; }
            set
            {
                _isPinned = value;
                OnPropertyChanged();
            }
        }


        public ICommand PostCommand => new Command(async () =>
        {
            var user = await _api.GetLoggedInUser();
            if(_postTitle != null && _postTitle != "" && _postText != null && _postText != "")
            {
                var post = new NewsPost()
                {
                    Title = _postTitle,
                    Text = _postText,
                    Picture = PostImage,
                    Author = user.FirstName + " " + user.LastName,
                    SenderId = user.Id,
                    Date = DateTime.Now,
                    Pinned = IsPinned,
                    SenderPic = user.ProfilePicture,
                };

                string desc = post.Text.Split('.', '\n').First();
                post.Description = desc;
                if(await _api.CreateNewsPost(post))
                {
                    Message = "En post har skapats!";
                    Application.Current.MainPage = new MainPage(new HomePage());
                    var posts = await _api.GetAllNewsPosts();
                    var latestPost = posts.Where(x => x.SenderId == post.SenderId && x.Text == post.Text && x.Title == post.Title && x.Date.ToLongDateString() == post.Date.ToLongDateString()).First();
                    await _api.AddUserToNewsPostViews(latestPost.Id, user.UserName);
                }
                else
                {
                    Message = "Något gick fel";
                }
            }
            else
            {
                Message = "Ange en titel och en text för din post!";
            }
        });

        public ICommand AddPicCommand => new Command(async () =>
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Message = "Att välja bild stöds inte på denna enhet";
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;
            }
            ImagePath = file.Path;
            PostImage = File.ReadAllBytes(ImagePath);
            IsVisible = true;
        });


        public ICommand RemovePicCommand => new Command(() =>
        {
            ImagePath = "";
            PostImage = null;
            IsVisible = false;
        });
    }
}
