using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class EditPostViewModel : BaseViewModel
    {
        public EditPostViewModel(NewsPost post)
        {
            IsGroupPost = false;
            EditPic = post.Picture;
            EditText = post.Text;
            EditTitle = post.Title;
            IsPinned = post.Pinned;
            Id = post.Id;

            if(EditPic != null)
            {
                IsRemovePicVisible = true;
                IsAddPicVisible = false;
            }
            else
            {
                IsRemovePicVisible = false;
                IsAddPicVisible = true;
            }
        }

        public EditPostViewModel(GroupPost post)
        {
            IsGroupPost = true;
            EditPic = post.Picture;
            EditText = post.Text;
            EditTitle = post.Title;
            IsPinned = post.Pinned;
            Id = post.Id;

            if (EditPic != null)
            {
                IsRemovePicVisible = true;
                IsAddPicVisible = false;
            }
            else
            {
                IsRemovePicVisible = false;
                IsAddPicVisible = true;
            }
        }

        private string _editText;

        public string EditText
        {
            get { return _editText; }
            set
            {
                _editText = value;
                OnPropertyChanged();
            }
        }

        private string _editTitle;

        public string EditTitle
        {
            get { return _editTitle; }
            set
            {
                _editTitle = value;
                OnPropertyChanged();
            }
        }

        private byte[] _editPic;

        public byte[] EditPic
        {
            get { return _editPic; }
            set
            {
                _editPic = value;
                OnPropertyChanged();
            }
        }

        public bool IsGroupPost { get; set; }

        public int Id { get; set; }

        private bool _isAddPicVisible;

        public bool IsAddPicVisible
        {
            get { return _isAddPicVisible; }
            set
            {
                _isAddPicVisible = value;
                OnPropertyChanged();
            }
        }


        private bool _isRemovePicVisible;

        public bool IsRemovePicVisible
        {
            get { return _isRemovePicVisible; }
            set
            {
                _isRemovePicVisible = value;
                OnPropertyChanged();
            }
        }


        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged();
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

        public  ICollection<User> Views { get; set; }

        public ICommand PostCommand => new Command(async () =>
        {
            if(EditTitle != "" && EditText != "")
            {
                if (IsGroupPost)
                {
                    var post = new EditPostModel()
                    {
                        Picture = EditPic,
                        Text = EditText,
                        Title = EditTitle,
                        Pinned = IsPinned,
                        Id = Id
                    };
                    post.Description = post.Text.Split('.', '\n').First();

                    var success = await _api.EditGroupPost(post);
                    if (success)
                    {
                        Application.Current.MainPage = new MainPage(new GroupPage());
                    }
                    else
                    {
                        Message = "Något gick fel :(";
                    }
                }
                else
                {
                    var post = new EditPostModel()
                    {
                        Picture = EditPic,
                        Text = EditText,
                        Title = EditTitle,
                        Pinned = IsPinned,
                        Id = Id
                    };
                    post.Description = post.Text.Split('.', '\n').First();

                    var success = await _api.EditNewsPost(post);
                    if (success)
                    {
                        Application.Current.MainPage = new MainPage(new HomePage());
                    }
                    else
                    {
                        Message = "Något gick fel :(";
                    }
                }
            }
            else
            {
                Message = "Titel och text får inte lämnas tomma";
            }
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            if (IsGroupPost)
            {
                var success = await _api.DeleteGroupPost(Id);
                if (success)
                {
                    Application.Current.MainPage = new MainPage(new GroupPage());
                }
                else
                {
                    Message = "Något gick fel :(";
                }
            }
            else
            {
                var success = await _api.DeleteNewsPost(Id);
                if (success)
                {
                    Application.Current.MainPage = new MainPage(new HomePage());
                }
                else
                {
                    Message = "Något gick fel :(";
                }
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
            EditPic = File.ReadAllBytes(ImagePath);
            IsRemovePicVisible = true;
            IsAddPicVisible = false;
        });

        public ICommand RemovePicCommand => new Command(() =>
        {
            ImagePath = "";
            EditPic = null;
            IsRemovePicVisible = false;
            IsAddPicVisible = true;
        });
    }
}
