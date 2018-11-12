using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Inspark.Models;
using Inspark.Services;
using Xamarin.Forms;

namespace Inspark.Viewmodels
{
	class HomeViewModel : BaseViewModel
	{
		private ObservableCollection<GroupPost> _groupPosts;

		public ObservableCollection<GroupPost> GroupPosts
		{
			get { return _groupPosts; }
			set
			{
				if (_groupPosts != value)
				{
					_groupPosts = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<NewsPost> _newsPosts;

		public ObservableCollection<NewsPost> NewsPosts
		{
			get { return _newsPosts; }
			set
			{
				if (_newsPosts != value)
				{
					_newsPosts = value;
					OnPropertyChanged();
				}
			}
		}

		private bool _newsIsRefreshing;

		public bool NewsIsRefreshing
		{
			get { return _newsIsRefreshing; }
			set
			{
				if (_newsIsRefreshing != value)
				{
					_newsIsRefreshing = value;
					OnPropertyChanged();
				}
			}
		}

		private bool _groupIsRefreshing;

		public bool GroupIsRefreshing
		{
			get { return _groupIsRefreshing; }
			set
			{
				if (_groupIsRefreshing != value)
				{
					_groupIsRefreshing = value;
					OnPropertyChanged();
				}
			}
		}


		public ICommand RefreshNewsCommand
		{
			get
			{
				return new Command(() =>
				{
					RefreshNewsListView();
				});
			}
		}

		public ICommand RefershGroupCommand
		{
			get
			{
				return new Command(() =>
				{
					RefreshGroupListView();
				});
			}
		}

		private async void RefreshNewsListView()
		{
			NewsIsRefreshing = true;
			var posts = await _api.GetAllNewsPosts();
			if (posts.Count < 1)
			{
				var post = new NewsPost()
				{
					Author = "Admin",
					Text = "Det finns inga poster ännu.",
					Title = "Det finns inga poster ännu.",
					Date = DateTime.Now,
					Picture = null,
				};
				posts.Add(post);
			}
			posts = new ObservableCollection<NewsPost>(posts.OrderByDescending(i => i.Date).OrderByDescending(i => i.Pinned));
			NewsPosts = posts;
			NewsIsRefreshing = false;
		}

		private async void RefreshGroupListView()
		{
			GroupIsRefreshing = true;
			var posts = await _api.GetAllGroupPosts();
			posts = new ObservableCollection<GroupPost>(posts.OrderByDescending(i => i.Date));
			if (posts.Count < 1)
			{
				var post = new GroupPost()
				{
					Author = "Admin",
					Text = "Det finns inga poster ännu.",
					Title = "Det finns inga poster ännu.",
					Date = DateTime.Now,
					Picture = null
				};
				posts.Add(post);
			}
			GroupPosts = posts;
			GroupIsRefreshing = false;
		}

		public HomeViewModel()
		{
			RefreshNewsListView();
			RefreshGroupListView();
		}
	}
}
