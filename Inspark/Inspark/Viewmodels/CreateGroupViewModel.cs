using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Inspark.Annotations;
using Inspark.Models;
using Inspark.Services;
using Inspark.Views;
using Plugin.Media;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    public class CreateGroupViewModel : BaseViewModel
    {
        private ObservableCollection<Section> _sectionList;

        public ObservableCollection<Section> SectionsList
        {
            get {return _sectionList; }
            set
            {
                if(_sectionList != value)
                {
                    _sectionList = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                OnPropertyChanged();
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

        private Section _groupSection;

        public Section GroupSection
        {
            get { return _groupSection; }
            set
            {
                _groupSection = value;
                OnPropertyChanged();
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

        private bool _isAddPicVisible;

        public bool IsAddPicVisible
        {
            get { return _isAddPicVisible; }
            set { _isAddPicVisible = value; OnPropertyChanged(); }
        }

        private bool _isRemovePicVisible;

        public bool IsRemovePicVisible
        {
            get { return _isRemovePicVisible; }
            set { _isRemovePicVisible = value; OnPropertyChanged(); }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(); }
        }

        private byte[] _groupPic;

        public byte[] GroupPic
        {
            get { return _groupPic; }
            set { _groupPic = value; }
        }


        public ICommand AddPicCommand => new Command(async () =>
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Message = "Att välja bild stöds inte på denna enhet.";
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;
            }
            ImagePath = file.Path;
            IsRemovePicVisible = true;
            IsAddPicVisible = false;
        });

        public ICommand RemovePicCommand => new Command(() =>
        {
            ImagePath = "";
            GroupPic = null;
            IsRemovePicVisible = false;
            IsAddPicVisible = true;
        });

        public ICommand GroupCommand => new Command(async () =>
        {
            var group = new Group()
            {
                Name = _groupName,
                IsIntroGroup = _isIntroGroup,
                Section = _groupSection
            };
            if (ImagePath == "")
            {
                var assembly = Assembly.GetExecutingAssembly();
                var pic = EmbeddedResourceToByteArray.GetEmbeddedResourceBytes(assembly, "group.png");
                GroupPic = pic;
            }
            else
            {
                GroupPic = File.ReadAllBytes(ImagePath);
            }
            group.GroupPic = GroupPic;
            if (await _api.CreateGroup(group))
            {
                Message = "Gruppen har skapats!";
                Application.Current.MainPage = new MainPage(new HomePage());
                var groupId = await _api.GetGroupIdByName(group.Name);
                if (groupId != -1)
                {
                    var groupChat = new GroupChat
                    {
                        GroupChatPic = group.GroupPic,
                        GroupName = group.Name,
                    };
                    await _api.CreateGroupChat(groupId, groupChat);               
                }
            }
            else
            {
                Message = "Något gick fel.";
            }
        });

        public async void PopulateLists()
        {
            IsAddPicVisible = true;
            IsRemovePicVisible = false;
            var sections = await _api.GetAllSections();
            sections = new ObservableCollection<Section>(sections);
            SectionsList = sections;
        }

        public CreateGroupViewModel()
        {
            PopulateLists();
        }
    }
}
