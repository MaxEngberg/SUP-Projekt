using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    class GroupViewModel : BaseViewModel
    {
        private Group _group;

        public Group Group
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<Group> _groups;

        public ObservableRangeCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }


        public User User { get; set; }

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

        public async void OnLoad()
        {
            Groups = new ObservableRangeCollection<Group>();
            User = await _api.GetLoggedInUser();
            if(User.Groups != null && User.Groups.Count != 0)
            {
                Groups.AddRange(User.Groups);
                if(Groups.Count > 1)
                {
                    Application.Current.MainPage = new MainPage(new SelectGroupPage());
                }
                else
                {
                    Group = Groups.First();
                }
            }
            if (Settings.UserRole == "Admin" || Settings.UserRole == "Fadder")
            {
                IsVisible = true;
            }
        }

        public GroupViewModel()
        {
            OnLoad();
        }

        public GroupViewModel(Group group)
        {

        }
    }
}
