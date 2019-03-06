using NewsApp.Extensions;
using NewsApp.Helpers;
using NewsApp.Model;
using NewsApp.View;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
        private bool Refreshed
        {
            get => _refreshed;
            set => SetValue(ref _refreshed, value);
        }
        //private Article _selectedArticle;
        private bool _refreshed;
        private IToast Toast = DependencyService.Get<IToast>();
        private bool _loading;
        public ICommand LoadArticles { get; private set; }
        public ICommand SelectArticle { get; private set; }
        public ICommand RefreshArticles { get; private set; }
        public ICommand ReadArticleCommand { get; private set; }
        public string Title => category.CategoryName.ToUpper();

        public NewsViewModel(NewsCategory category, IServices service)
        {
            this.category = category;
            _loading = true;
            services = service;
            _articles = new ObservableCollection<Article>();
            LoadArticles = new Command(async () => await GetNews());
            //SelectArticle = new Command(async () => await LoadArticle());
            RefreshArticles = new Command(async () => await Refresh());
            ReadArticleCommand = new Command<Article>(async vm => await ReadArticle(vm));
        }

        
        //public Article SelectedArticle
        //{
        //    get => _selectedArticle;
        //    set
        //    {
        //        _selectedArticle = value;
        //    }
        //}
        public bool Loading
        {
            get => _loading;
            set => SetValue(ref _loading, value);
        }
        public ObservableCollection<Article> Articles { get => _articles; }

        private async Task<bool> GetNews()
        {
            var news = new ObservableCollection<Article>();
            string url;
            if (category.CategoryName.ToLower() == topstories)
                url = Constants.TopStories;
            else
                url = Constants.CategoryStories(category.CategoryName.ToLower());
            if(CrossConnectivity.IsSupported)
            {
                if(Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    Toast.Show("No Internet Connection");
                    Loading = false;
                    return false;
                }
            }
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(20);
                try
                {
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<News>(json);
                        if (result.Status == StatusCode.error)
                        {
                            Toast.Show("Cannot retrieve news feed at the moment");
                            Loading = false;
                            return false;
                        }
                        news = result.Articles;
                    }
                }
                catch(Exception)
                {
                    //Console.WriteLine(e.Source);
                    //Console.WriteLine(e.Message);
                    return false;
                }
            }

            var empty = Articles.Where(x => x.UrlToImage == null);
            foreach (var image in empty)
            {
                image.UrlToImage = null;
            }
            foreach (var article in news)
                _articles.Add(article);
            Loading = false;
            return true;
        }

        #region Implementation of Article Selection
        //private async Task LoadArticle()
        //{
        //    if (_selectedArticle == null)
        //        return;
        //    var model = new WebViewModel(_selectedArticle.Url, services);
        //    await services.NavigationPushAsync(new WebPage(model));
        //    SetValue(ref _selectedArticle, null);
        //    OnPropertyChanged(nameof(SelectedArticle));
        //}
        #endregion

        private async Task Refresh()
        {
            var gotNews = await GetNews();
            if(gotNews)
                _articles.Clear();
            Refreshed = true;
        }

        private async Task ReadArticle(Article article)
        {
            await Xamarin.Essentials.Browser.OpenAsync(article.Url, Xamarin.Essentials.BrowserLaunchMode.SystemPreferred);
        }
    }
}
