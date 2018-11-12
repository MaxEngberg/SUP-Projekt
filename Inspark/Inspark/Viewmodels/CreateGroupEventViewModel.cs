using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Inspark.Helpers;
using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class CreateGroupEventViewModel : BaseViewModel
    {
        public CreateGroupEventViewModel()
        {
            PopulateLists();
        }

        public string Title { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        // public IEnumerable<User> Invited { get; set; }
        //public IEnumerable<User> Attending { get; set; }
        public string Description { get; set; }


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

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        private bool _IsButtonEnabled { get; set; }
        public bool IsButtonEnabled
        {
            get { return _IsButtonEnabled; }
            set
            {
                _IsButtonEnabled = value;
                OnPropertyChanged();
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

        private string _senderId;

        public string SenderId
        {
            get { return _senderId; }
            set
            {
                _senderId = value;
                OnPropertyChanged();
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

        public async void PopulateLists()
        {

            var groups = await _api.GetAllGroupsByUserId();
            groups = new ObservableCollection<Group>(groups);
            Groups = groups;
        }

        public ICommand CreateEventCommand => new Command(async () =>
        {
            DateTime newDateTime = StartDate.Date.Add(StartTime);
            var a = newDateTime;
            var b = newDateTime;
            Date = newDateTime;
            GroupEvent groupEvent = new GroupEvent
            {
                Title = Title,
                Location = Location,
                TimeForEvent = Date,
                Description = Description,
                senderId = Settings.UserId,
                GroupId = Groups[SelectedIndex].Id
            };
            var isSuccess = await _api.CreateGroupEvent(groupEvent);

            if (isSuccess)
            {
                Application.Current.MainPage = new MainPage(new GroupPage());
            }
            else
            {
                Message = "Något gick fel";
            }
        });

    }
}
