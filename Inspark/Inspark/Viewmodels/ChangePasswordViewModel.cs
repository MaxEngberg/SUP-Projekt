using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    class ChangePasswordViewModel : BaseViewModel
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _confirmNewPassword;

        public string ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set
            {
                if (_confirmNewPassword != value)
                {
                    _confirmNewPassword = value;
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
                if (IsLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _currentPassword;

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                if (_currentPassword != value)
                {
                    _currentPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ConfirmCommand => new Command(async () =>
        {
            IsLoading = true;
            if(NewPassword != "" && NewPassword != null && ConfirmNewPassword != "" && ConfirmNewPassword != null)
            {
                if (PasswordBehavior.IsValidPassword(NewPassword))
                {
                    if(PasswordBehavior.IsPasswordMatch(NewPassword, ConfirmNewPassword))
                    {
                        if(PasswordBehavior.IsPasswordMatch(CurrentPassword, Settings.UserPassword))
                        {
                            var user = await _api.GetLoggedInUser();
                            var model = new ChangePasswordModel()
                            {
                                NewPassword = NewPassword,
                                OldPassword = CurrentPassword,
                                ConfirmPassword = ConfirmNewPassword,
                                Id = user.Id
                            };
                            if (await _api.ChangePassword(model))
                            {
                                Settings.UserPassword = NewPassword;
                                Message = "Lösenord bytt!";
                                IsLoading = false;
                            }
                            else
                            {
                                Message = "RIP :(";
                                IsLoading = false;
                            }
                        }
                        else
                        {
                            Message = "Fel lösenord.";
                            IsLoading = false;
                        }
                        
                    }
                    else
                    {
                        Message = "Dina nya lösenord stämmer inte överens.";
                        IsLoading = false;
                    }
                }
                else
                {
                    Message = "Ditt lösenord måste innehålla en siffra, en versal, en gemen och bestå av minst 6 tecken";
                    IsLoading = false;
                }
            }
            else
            {
                Message = "Ange ett nytt lösenord och bekräfta det.";
                IsLoading = false;
            }
        });
    }
}
