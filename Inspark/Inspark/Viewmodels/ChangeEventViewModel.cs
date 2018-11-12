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
    public class ChangeEventViewModel : BaseViewModel
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

        private ObservableCollection<Event> _events;

        public ObservableCollection<Event> Events
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

        private Event _selectedEvent;

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                if (_selectedEvent != value)
                {
                    _selectedEvent = value;
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
            Title = SelectedEvent.Title;
            Location = SelectedEvent.Location;
            Description = SelectedEvent.Description;
            StartDate = SelectedEvent.TimeForEvent.Date;
            StartTime = SelectedEvent.TimeForEvent.TimeOfDay;
        });

        public async void PopulateList()
        {
            var events = await _api.GetAllEvents();
            var events2 = events.Where(x => x.SenderId == Settings.UserId);
            events = new ObservableCollection<Event>(events2);
            Events = events;
        }

        public ChangeEventViewModel()
        {
            PopulateList();
        }

        public ICommand ChangeEvent => new Command(async () =>
        {
            DateTime newDateTime = StartDate.Date.Add(StartTime);

            var events = new Event
            {
                Id = SelectedEvent.Id,
                Title = Title,
                Location = Location,
                TimeForEvent = newDateTime,
                Description = Description
                
            };
            var isSuccess = await _api.ChangeEvent(events);
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
