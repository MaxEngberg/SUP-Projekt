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
    public class DeleteGroupEventViewModel : BaseViewModel
    {
        private ObservableCollection<GroupEvent> _groupEventList;

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

        public async void PopulateList()
        {
            
            _groupEventList = await _api.GetAllGroupEvents();
			if (_groupEvents.Count() < 1)
			{

			}
			else{
				var events = _groupEventList.Where(x => x.senderId == Settings.UserId);
                GroupEvents = new ObservableCollection<GroupEvent>(events);
			}

        }

        public DeleteGroupEventViewModel()
        {
            _groupEventList = new ObservableCollection<GroupEvent>();
            PopulateList();
        }

        public ICommand DeleteGroupEvent => new Command(async () =>
        {
            var eventId = GroupEvents[SelectedIndex].Id;
            var isSuccess = await _api.DeleteGroupEvent(eventId);
            if (!isSuccess)
            {
                Message = "Något Gick fel";
            }
            else
            {
                Application.Current.MainPage = new MainPage(new HomePage());
            }
        });
    }
}
