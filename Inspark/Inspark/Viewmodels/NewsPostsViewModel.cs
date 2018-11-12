using Inspark.Models;
using Inspark.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Inspark.Services;
using System.Linq;

namespace Inspark.Viewmodels
{
    public class NewsPostsViewModel : BaseViewModel
    {
        private ObservableCollection<NewsPost> _newsPosts;

        public ObservableCollection<NewsPost> NewsPosts
        {

            get { return _newsPosts; }
            set
            {
                if(_newsPosts != value)
                {
                    _newsPosts = value;
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
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private NewsPost _itemSelected;

        public NewsPost ItemSelected
        {
            get { return _itemSelected; }
            set
            {
                if(_itemSelected != value)
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
                if(_isVisible != value)
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
            var posts = await _api.GetAllNewsPosts();
            if(posts.Count < 1)
            {
                var post = new NewsPost()
                {
                    Author = "Admin",
                    Text = "Det finns inga poster ännu.",
                    Title = "Det finns inga poster ännu.",
                    Date = DateTime.Now,
                    Picture = null,
                    Pinned = true
                };
                posts.Add(post);
            }
            posts = new ObservableCollection<NewsPost>(posts.OrderByDescending(i => i.Date).OrderByDescending(i => i.Pinned));
            NewsPosts = posts;
            IsRefreshing = false;
        }

        public NewsPostsViewModel()
        {
            RefreshListView();
        }
    }
}
