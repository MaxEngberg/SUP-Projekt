using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Inspark.Models;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
	public class GroupMembersViewModel : BaseViewModel
    {
        public GroupMembersViewModel()
        {
			Members = new ObservableCollection<User>();
			Populatelist();
        }

		private async void Populatelist()
		{
			var groups = await _api.GetAllGroupsByUserId();
            groups = new ObservableCollection<Group>(groups);
            Groups = groups;
		}

		private ObservableCollection<User> _members;
        
        public ObservableCollection<User> Members
        {
			get { return _members; }
            set
            {
				if (_members != value)
                {
					_members = value;
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

		private Group _SelectedGroup;
        
        public Group SelectedGroup
        {
			get { return _SelectedGroup; }
            set
            {
				if (_SelectedGroup != value)
                {
					_SelectedGroup = value;
                    OnPropertyChanged();
                }
            }
        }

		public ICommand SelectedProviderChanged => new Command(() =>
        {
			var group = Groups.Where(x => x.Name == SelectedGroup.Name).SelectMany(x => x.Users);

			foreach (var item in group)
			{
				Members.Add(item);
			}
        });
	}
}
