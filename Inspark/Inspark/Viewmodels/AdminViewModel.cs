using Inspark.Models;
using Inspark.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class AdminViewModel : BaseViewModel
    {
        public async void OnLoad()
        {
            Users = await _api.GetAllUsers();
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<User> _suggestions;

        public ObservableCollection<User> Suggestions
        {
            get { return _suggestions; }
            set
            {
                _suggestions = value;
                OnPropertyChanged();
            }
        }

        public Command SearchCommand
        {
            get
            {
                return new Command(Search);
            }
        }

        private void Search()
        {
            if (_keyword.Length >= 1)
            {
                var suggestionsList = Users.Where(c => c.FirstName.ToLower().Contains
                 (_keyword.ToLower()) || c.LastName.ToLower().Contains(_keyword.ToLower()));

                var suggestionListCollection = new ObservableCollection<User>(suggestionsList);
                Suggestions = suggestionListCollection;

                IsVisible = true;
            }
            else
            {
                IsVisible = false;
            }
        }

        public AdminViewModel()
        {
            OnLoad();
        }
    }
}
