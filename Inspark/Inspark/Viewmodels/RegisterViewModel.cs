using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Inspark.Services;
using Plugin.Media;
using System.IO;
using Inspark.Helpers;
using Inspark.Models;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Inspark.Viewmodels
{
    public class RegisterViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public byte[] Pic { get; set; }

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

        private ObservableCollection<Section> _sectionList;

        public ObservableCollection<Section> SectionsList
        {
            get { return _sectionList; }
            set
            {
                if (_sectionList != value)
                {
                    _sectionList = value;
                    OnPropertyChanged();
                }
            }
        }

        public RegisterViewModel()
        {
            OnLoad();
        }

        public async void OnLoad()
        {
            SectionsList = await _api.GetAllSections();
            IsRemovePicVisible = false;
            IsAddPicVisible = true;
            ImagePath = "";
        }

        public bool IsLoggedIn { get; set; }

        private Section _section;

        public Section Section
        {
            get { return _section; }
            set
            {
                if(_section != value)
                {
                    _section = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRemovePicVisible;

        public bool IsRemovePicVisible
        {
            get { return _isRemovePicVisible; }
            set
            {
                if(_isRemovePicVisible != value)
                {
                    _isRemovePicVisible = value;
                    OnPropertyChanged();
                }
                
            }
        }

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

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if(_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddPicCommand => new Command(async () =>
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Message = "Att välja bild stöds inte på denna enhet.";
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if(file == null)
            {
                return;
            }
            ImagePath = file.Path;
            IsRemovePicVisible = true;
            IsAddPicVisible = false;
        });

        public ICommand RemovePicCommand => new Command(() =>
        {
            ImagePath = "";
            Pic = null;
            IsRemovePicVisible = false;
            IsAddPicVisible = true;
        });

        public ICommand RegisterCommand => new Command(async () =>
        {
            if(TextOnlyBehavior.IsTextOnly(FirstName) && TextOnlyBehavior.IsTextOnly(LastName) && EmailBehaviors.IsEmail(Email) && NumberBehavior.IsNumbers(PhoneNumber) && PasswordBehavior.IsValidPassword(Password) && PasswordBehavior.IsPasswordMatch(Password, ConfirmPassword))
            {
                if(Section != null)
                {
                    IsLoading = true;
                    if(ImagePath == "")
                    {
                        var assembly = Assembly.GetExecutingAssembly();
                        var pic = EmbeddedResourceToByteArray.GetEmbeddedResourceBytes(assembly, "profile.png");
                        Pic = pic;
                    }
                    else
                    {
                        Pic = File.ReadAllBytes(ImagePath);
                    }

                    var user = new User
                    {
                        UserName = Email,
                        Password = Password,
                        Email = Email,
                        Section = Section.Name,
                        FirstName = FirstName,
                        LastName = LastName,
                        Role = "Admin",
                        ConfirmPassword = Password,
                        PhoneNumber = PhoneNumber,
                        ProfilePicture = Pic,
                    };
                    var isSuccess = await _api.RegisterAsync(user);

                    if (isSuccess)
                    {
                        Settings.UserName = Email;
                        Settings.UserPassword = Password;
						var loggedInUser = await _api.GetLoggedInUser();
						Settings.UserId = loggedInUser.Id;
						Settings.UserRole = loggedInUser.Role;
                        var page = new Views.MainPage(new Views.HomePage());
                        NavigationPage.SetHasNavigationBar(page, false);
                        await Application.Current.MainPage.Navigation.PushAsync(page);
                        IsLoading = false;
                    }
                    else
                    {
                        Message = "Försök igen!";
                        IsLoading = false;
                    }
                }
                else
                {
                    Message = "Välj den sektion du tillhör!";
                }
            }
            else
            {
                Message = "Ett eller flera inmatningsfält stämmer inte. Kontrollera dina uppgifter.";
            }
        });
    }
}
