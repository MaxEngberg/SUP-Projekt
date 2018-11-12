using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Inspark.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using XamForms.Controls;
using System.Linq;

namespace Inspark.Viewmodels
{
	public class ScheduleViewModel : BaseViewModel
	{

		private ObservableCollection<CombindEvent> _specificDates;

		public ObservableCollection<CombindEvent> SpecificDates
		{
			get { return _specificDates; }
			set
			{
				if (_specificDates != value)
				{
					_specificDates = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<GroupEvent> _groupsEvent;

		public ObservableCollection<GroupEvent> GroupEvent
		{
			get { return _groupsEvent; }
			set
			{
				if (_groupsEvent != value)
				{
					_groupsEvent = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<Event> _event;

		public ObservableCollection<Event> Event
		{
			get { return _event; }
			set
			{
				if (_event != value)
				{
					_event = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<SpecialDate> _events;

		public ObservableCollection<SpecialDate> Events
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

		private DateTime _date;

		public DateTime Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				OnPropertyChanged();
			}
		}

		public async void populateLists()
		{
			var events = await _api.GetAllEventsAttending();
			Event = events;
			var groupEvents = await _api.GetAllGroupEventsAttending();
			GroupEvent = groupEvents;

			if (Event.Count() > 0)
			{
				foreach (var item in Event)
				{
					var specialDate = new SpecialDate(item.TimeForEvent);
					specialDate.BackgroundColor = Color.Green;
					specialDate.TextColor = Color.White;
					specialDate.Selectable = true;
					Events.Add(specialDate);
				}
			}

			if (GroupEvent.Count() > 0)
			{
				foreach (var item in GroupEvent)
				{
					var specialDate = new SpecialDate(item.TimeForEvent);
					specialDate.BackgroundColor = Color.Green;
					specialDate.TextColor = Color.White;
					specialDate.Selectable = true;
					Events.Add(specialDate);
				}
			}


		}

		public ScheduleViewModel()
		{
			Events = new ObservableCollection<SpecialDate>();
			SpecificDates = new ObservableCollection<CombindEvent>();
			Event = new ObservableCollection<Event>();
			GroupEvent = new ObservableCollection<GroupEvent>();
			populateLists();
		}


		public ICommand DateChosen => new Command((obj) =>
		{

			SpecificDates.Clear();
			foreach (var item in Event)
			{
				if (item.TimeForEvent.ToString("yyyy/MM/dd") == Date.ToString("yyyy/MM/dd"))
				{
					var model = new CombindEvent
					{
						Title = item.Title,
						TimeForEvent = item.TimeForEvent,
						Id = item.Id,
						Type = "Event"
					};
					SpecificDates.Add(model);
				}
			}
			foreach (var item in GroupEvent)
			{
				if (item.TimeForEvent.ToString("yyyy/MM/dd") == Date.ToString("yyyy/MM/dd"))
				{
					var model = new CombindEvent
					{
						Title = item.Title,
						TimeForEvent = item.TimeForEvent,
						Id = item.Id,
						Type = "GroupEvent"
					};
					SpecificDates.Add(model);
				}
			}

		});

	}
}




