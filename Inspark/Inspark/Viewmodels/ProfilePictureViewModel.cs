using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Inspark.Helpers;
using Xamarin.Forms;
using Inspark.Models;
using Inspark.Views;

namespace Inspark.Viewmodels
{
    class ProfilePictureViewModel : BaseViewModel
    {
        private string _imagePath;

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
        
		private User _user;

        public User User
        {
			get { return _user; }
            set
            {
				if (_user != value)
                {
					_user = value;
                    OnPropertyChanged();
                }
            }
        }

		public ICommand PickPhotoCommand => new Command(async () =>
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;
            }
            ImagePath = file.Path;
            var bild = File.ReadAllBytes(ImagePath);
        });

        public ICommand LogOutCommand
        {
            get
            {
                return new Command(() =>
                    {
                        Settings.AccessToken = "";
                        Settings.UserName = "";
                        Settings.UserPassword = "";
                    });
            }
        }
    }
}
