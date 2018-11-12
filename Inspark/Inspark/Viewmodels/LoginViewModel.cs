using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using Inspark.Models;
using Inspark.Views;
using Inspark.Services;
using System.Linq;
using System.Collections;
using System.Net;
using Inspark.Helpers;

namespace Inspark.Viewmodels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _password = "";

        public string Password
        {
            get { return _password; }
            set
            {
                if(_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
                
            }
        }

        private string _email = "";

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _keepLoggedIn;

        public bool KeepLoggedIn
        { 
            get 
            {
                return _keepLoggedIn;
            } set
            {
                _keepLoggedIn = value;
                OnPropertyChanged();
            } 
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _alertMessage;

        public string AlertMessage
        {
            get { return _alertMessage; }
            set
            {
                if(_alertMessage != value)
                {
                    _alertMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginClick => new Command(async () =>
        {
            if (_password != null)
            {
                if (Email != null)
                {
                    IsLoading = true;
                    var response =  await _api.LoginAsync(Email, Password);
                    if (response)
                    {
                        if (Settings.UserName != Email)
                        {
                            Settings.UserName = Email;
                            Settings.UserPassword = Password;
                        }
                        IsLoading = false;
                        var user = await _api.GetLoggedInUser();
                        var userId = user.Id;
                        Settings.UserId = userId;
                        Settings.UserRole = user.Role;
                        Application.Current.MainPage = new MainPage(new HomePage());
                    }
                    else
                    {
                        IsLoading = false;
                        AlertMessage = "Fel Email eller lösenord";
                    }
                }
            }
        });

        public LoginViewModel()
        {
            Email = Settings.UserName;
            Password = Settings.UserPassword;
        }
    }
}
