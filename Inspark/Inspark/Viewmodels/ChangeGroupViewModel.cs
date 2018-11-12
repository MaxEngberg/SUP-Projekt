using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class ChangeGroupViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isIntroGroup;

        public bool IsIntroGroup
        {
            get { return _isIntroGroup; }
            set
            {
                _isIntroGroup = value;
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

        private ObservableCollection<Section> _sectionList;

        public ObservableCollection<Section> SectionsList
        {
            get { return _sectionList; }
            set
            {
                if (_sectionList != value)
                {
                    _sectionList = value;
                    OnPropertyChanged();
                }
            }
        }

        private Section _selectedSection;

        public Section SelectedSections
        {
            get { return _selectedSection; }
            set
            {
                if (_selectedSection != value)
                {
                    _selectedSection = value;
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

        public ICommand SelectedProviderChanged => new Command(() =>
        {
            Name = SelectedGroup.Name;
            IsIntroGroup = SelectedGroup.IsIntroGroup;
       
            var q = SectionsList.IndexOf(SectionsList.Where(X => X.Id == SelectedGroup.SectionId).FirstOrDefault());
            if (q > -1)
            {
                SelectedIndex = q;
            }
            else
            {
                
            }
        });

        public ICommand ChangeGroup => new Command(async () =>
        {

            var group = new Group
            {
                Id = SelectedGroup.Id,
                Name = Name,
                IsIntroGroup = IsIntroGroup,
                SectionId = SectionsList[SelectedIndex].Id
            };
            var isSuccess = await _api.ChangeGroup(group);
            if (isSuccess)
            {
                Application.Current.MainPage = new MainPage(new AdminPage());
            }
            else
            {
                Message = "Något Gick Fel";
            }
        });

        public async void PopulateLists()
        {
            var groups = await _api.GetAllGroups();
            groups = new ObservableCollection<Group>(groups);
            Groups = groups;
            var sections = await _api.GetAllSections();
            sections = new ObservableCollection<Section>(sections);
            SectionsList = sections;
        }

        public ChangeGroupViewModel()
        {
            PopulateLists();
        }
    }
}
