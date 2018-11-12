using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class ChangeGroupEventViewModel : BaseViewModel
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _startTime;

        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GroupEvent> _groupEvents;

        public ObservableCollection<GroupEvent> GroupEvents
        {
            get { return _groupEvents; }
            set
            {
                if (_groupEvents != value)
                {
                    _groupEvents = value;
                    OnPropertyChanged();
                }
            }
        }

        private GroupEvent _selectedGroupEvent;

        public GroupEvent SelectedGroupEvent
        {
            get { return _selectedGroupEvent; }
            set
            {
                if (_selectedGroupEvent != value)
                {
                    _selectedGroupEvent = value;
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

        public ICommand SelectedProviderChanged => new Command(() =>
        {
            Title = SelectedGroupEvent.Title;
            Location = SelectedGroupEvent.Location;
            Description = SelectedGroupEvent.Description;
            StartDate = SelectedGroupEvent.TimeForEvent.Date;
            StartTime = SelectedGroupEvent.TimeForEvent.TimeOfDay;
        });

        public async void PopulateList()
        {
			var events = await _api.GetAllGroupEvents();
			if (events.Count() > 0)
			{
				var events2 = events.Where(x => x.senderId == Settings.UserId);
                events = new ObservableCollection<GroupEvent>(events2);
                GroupEvents = events;
			}

        }

        public ChangeGroupEventViewModel()
        {
            PopulateList();
        }

        public ICommand ChangeGroupEvent => new Command(async () =>
        {
            DateTime newDateTime = StartDate.Date.Add(StartTime);

            var groupEvents = new GroupEvent
            {
                Id = SelectedGroupEvent.Id,
                Title = Title,
                Location = Location,
                TimeForEvent = newDateTime,
                Description = Description

            };
            var isSuccess = await _api.ChangeGroupEvent(groupEvents);
            if (isSuccess)
            {
                Application.Current.MainPage = new MainPage(new AdminPage());
            }
            else
            {
                Message = "Något Gick Fel";
            }
        });
    }
}
