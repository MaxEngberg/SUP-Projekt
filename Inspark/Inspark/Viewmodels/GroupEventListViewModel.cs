using System;
using System.Collections.ObjectModel;
using System.Linq;
using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class GroupEventListViewModel : BaseViewModel
    {
        public User User { get; set; }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GroupEvent> _events;

        public ObservableCollection<GroupEvent> Events
        {
            get { return _events; }
            set
            {
                if (_events != value)
                {
                    _events = value;
                    OnPropertyChanged();
                }
            }
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


        public async void PopulateList()
        {
            var groups = await _api.GetAllGroupsByUserId();
            groups = new ObservableCollection<Group>(groups);
            Groups = groups;
            var events = await _api.GetAllGroupEvents();
            if (events.Count < 1)
            {
                var e = new GroupEvent()
                {
                    Title = "Inga event ännu",
                };
                events.Add(e);
            }
            var events2 = new ObservableCollection<GroupEvent>();
            foreach (var item in events)
            {
                foreach (var i in Groups)
                {
                    if (item.GroupId == i.Id)
                    {
                        events2.Add(item);
                    }
                }
            }
            Events = new ObservableCollection<GroupEvent>(events2.OrderByDescending(i => i.TimeForEvent));
            Suggestions = events2;
        }

        public Command EmptyCommand
        {
            get
            {
                return new Command(Empty);
            }
        }

        public Command SearchCommand
        {
            get
            {
                return new Command(Search);
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

        private ObservableCollection<GroupEvent> _suggestions;

        public ObservableCollection<GroupEvent> Suggestions
        {
            get { return _suggestions; }
            set
            {
                _suggestions = value;
                OnPropertyChanged();
            }
        }

        private void Empty()
        {
            Suggestions = Events;
        }

        private void Search()
        {
            if (_keyword.Length >= 1)
            {
                var suggestionsList = Events.Where(c => c.Title.ToLower().Contains
                 (_keyword.ToLower()));

                var suggestionListCollection = new ObservableCollection<GroupEvent>(suggestionsList);
                Suggestions = suggestionListCollection;


            }
            else
            {

            }
        }

        public async void OnLoad()
        {
            User = await _api.GetLoggedInUser();

            if (User.Role == "Admin")
            {
                IsAdmin = true;
            }
            else
            {
                IsAdmin = false;
            }
        }

        public GroupEventListViewModel()
        {
            PopulateList();
            OnLoad();
        }
    }
}
