using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Inspark.Models;
using Inspark.Services;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class DeleteUserFromGroupViewModel : BaseViewModel
    {
        public DeleteUserFromGroupViewModel()
        {
            PopulateLists();
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                if (_groups != value)
                {
                    _groups = value;
                    OnPropertyChanged();
                }
            }
        }

        private User _selectedUsers;

        public User SelectedUser
        {
            get { return _selectedUsers; }
            set
            {
                if (_selectedUsers != value)
                {
                    _selectedUsers = value;
                    OnPropertyChanged();
                }

            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged();
                }

            }
        }

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

        public async void PopulateLists()
        {
            var groups = await _api.GetAllGroups();
            groups = new ObservableCollection<Group>(groups);
            Groups = groups;
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged();
                }

            }
        }

        public ICommand SelectedProviderChanged => new Command(() =>
        {
            Users = new ObservableCollection<User>();
            var User = Groups[SelectedIndex].Users;
            if (User.Count > 0)
            {
                foreach (var item in User)
                {
                    Users.Add(item);
                }
            }
            else
            {

            }

        });

        public ICommand DeleteUser => new Command(async () =>
        {
            var userId = SelectedUser.Id;
            var gruppId = Groups[SelectedIndex].Id;
            var isSuccess = await _api.DeleteUserFromGroup(gruppId, userId);
            if (isSuccess)
            {
                Message = "Användaren har Tagits bort";
                Users.Remove(SelectedUser);
                Groups[SelectedIndex].Users.Remove(SelectedUser);
            }
            else
            {
                Message = "Något Gick Fel";
            }
        });

    }
    }

