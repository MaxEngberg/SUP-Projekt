using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class DeleteGroupViewModel : BaseViewModel
    {
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

        private Group _selectedGroup;

        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (_selectedGroup != value)
                {
                    _selectedGroup = value;
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

        public async void PopulateLists()
        {

            var groups = await _api.GetAllGroups();
            groups = new ObservableCollection<Group>(groups);
            Groups = groups;
        }

        public DeleteGroupViewModel()
        {
            PopulateLists();
        }

        public ICommand DeleteGroup => new Command(async () =>
        {
            var groupId = SelectedGroup.Id;
            var isSuccess = await _api.DeleteGroup(groupId);
            if (!isSuccess)
            {
                Message = "Något Gick fel";
            }
            else
            {
                Application.Current.MainPage = new MainPage(new AdminPage());
            }
        });
    }
}
