using Inspark.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Inspark.Viewmodels
{
    public class CompetitionViewModel : BaseViewModel
    {
        public User User { get; set; }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Score> _score;

        public ObservableCollection<Score> Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Competition> _competition;

        public ObservableCollection<Competition> Competition
        {
            get { return _competition; }
            set
            {
                if (_competition != value)
                {
                    _competition = value;
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

        public async void OnLoad()
        {
            User = await _api.GetLoggedInUser();

            if (User.Role == "Admin")
            {
                IsAdmin = true;
            }
            else
            {
                IsAdmin = false;
            }
        }

        public async void LoadResult()
        {
            var score = await _api.GetAllScore();
            score = new ObservableCollection<Score>(score.OrderByDescending(i => i.TotalPoints));
            Score = score;

            var groups = await _api.GetAllGroups();
            groups = new ObservableCollection<Group>(groups);
            Groups = groups;

            foreach (var item in Score)
            {
                foreach (var i in Groups)
                {
                    if (i.Id == item.Id)
                    {
                        var comp = new Competition
                        {
                            Name = i.Name,
                            TotalScore = item.TotalPoints
                        };

                        if (i.IsIntroGroup == true)
                        {
                            Competition.Add(comp);
                        }
                    }
                }
            }
        }

        public CompetitionViewModel()
        {
            Competition = new ObservableCollection<Competition>();
            OnLoad();
            LoadResult();
        }
    }
}
