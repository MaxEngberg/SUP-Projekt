using Inspark.Services;
using System;
using System.Windows.Input;
using Inspark.Views;
using Xamarin.Forms;
using Inspark.Helpers;

namespace Inspark.Viewmodels
{
    public class CreateEventViewModel : BaseViewModel
    {        
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

        public ICommand CreateEventCommand => new Command(async () =>
        {
            
            DateTime newDateTime = StartDate.Date.Add(StartTime);
            Date = newDateTime;
            var isSuccess = await _api.CreateEvent(Title, Location, Date, Description, SenderId);

            if (isSuccess)
            {
                Application.Current.MainPage = new MainPage(new EventListPage());
            }
            else
            {
                Message = "Något gick fel";
            }    
        });

        public async void GetLoggedinUser()
        {
            var user = await _api.GetLoggedInUser();
            SenderId = user.Id;
        }

        public CreateEventViewModel()
        {
            GetLoggedinUser();
        }
    }
    
}