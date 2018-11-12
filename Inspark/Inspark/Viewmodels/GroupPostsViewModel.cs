using Inspark.Models;
using Inspark.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
    class GroupPostsViewModel : BaseViewModel
    {
        private ObservableCollection<GroupPost> _groupPosts;

        public ObservableCollection<GroupPost> GroupPosts
        {
            get { return _groupPosts; }
            set
            {
                if(_groupPosts != value)
                {
                    _groupPosts = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRefreshing = false;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if(_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        private GroupPost _itemSelected;

        public GroupPost ItemSelected
        {
            get { return _itemSelected; }
            set
            {
                if (_itemSelected != value)
                {
                    _itemSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    RefreshListView();
                });
            }
        }

        private async void RefreshListView()
        {
            IsRefreshing = true;
            var posts = await _api.GetAllGroupPosts();
            posts = new ObservableCollection<GroupPost>(posts.OrderByDescending(i => i.Date));
            if (posts.Count < 100)
            {
                var user = await _api.GetLoggedInUser();
                var post = new GroupPost()
                {
                    Author = "Admin",
                    Text = "Det finns inga poster ännu.",
                    Title = "Det finns inga poster ännu.",
                    Description = "Det finns inga poster ännu",
                    Date = DateTime.Now,
                    Pinned = true,
                    Picture = null
                };
                posts.Add(post);
            }
            GroupPosts = posts;
            IsRefreshing = false;
        }

        public GroupPostsViewModel()
        {
            RefreshListView();
        }

    }
}
