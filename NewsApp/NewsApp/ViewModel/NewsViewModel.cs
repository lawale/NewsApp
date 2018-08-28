using NewsApp.Extensions;
using NewsApp.Helpers;
using NewsApp.Model;
using NewsApp.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static NewsApp.Helpers.Categories;

namespace NewsApp.ViewModel
{
    public class NewsViewModel : BaseViewModel
    {
        public readonly NewsCategory category;
        private readonly IServices services;
        private ObservableCollection<Article> _articles;
        private bool Loaded { get; set; }
        private bool Refreshed { get; set; }
        private Article _selectedArticle;
        private bool _refreshed;
        private bool _loading;
        public ICommand LoadArticles { get; private set; }
        public ICommand SelectArticle { get; private set; }
        public ICommand RefreshArticles { get; private set; }

        public NewsViewModel(NewsCategory category, IServices service)
        {
            this.category = category;
            _loading = true;
            services = service;
            _articles = new ObservableCollection<Article>();
            LoadArticles = new Command(async () => await GetNews());
            SelectArticle = new Command(async () => await LoadArticle());
            RefreshArticles = new Command(async () => await Refresh());
        }

        public Article SelectedArticle
        {
            get
            {
                return _selectedArticle;
            }
            set
            {
                _selectedArticle = value;
            }
        }
        public bool Loading
        {
            get
            {
                return _loading;
            }
        }
        public ObservableCollection<Article> Articles
        {
            get
            {
                return _articles;
            }
        }

        private async Task GetNews()
        {
            var news = new ObservableCollection<Article>();
            string url;
            if (category.CategoryName.ToLower() == topstories)
                url = Constants.TopStories;
            else
                url = Constants.CategoryStories(category.CategoryName.ToLower());
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<News>(json);
                news = result.Articles;
            }
            var empty = Articles.Where(x => x.UrlToImage == null);
            foreach (var image in empty)
            {
                image.UrlToImage = null;
            }
            foreach (var article in news)
                _articles.Add(article);
            SetValue(ref _loading, false);
            OnPropertyChanged(nameof(Loading));
        }

        private async Task LoadArticle()
        {
            if (_selectedArticle == null)
                return;
            var model = new WebViewModel(_selectedArticle.Url, services);
            await services.NavigationPushAsync(new WebPage(model));
            SetValue(ref _selectedArticle, null);
            OnPropertyChanged(nameof(SelectedArticle));
        }

        private async Task Refresh()
        {
            _articles.Clear();
            await GetNews();
            SetValue(ref _refreshed, true);
            OnPropertyChanged(nameof(Refreshed));
        }
    }
}
