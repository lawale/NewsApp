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

namespace NewsApp.ViewModel
{
    public class TopHeadlinesViewModel : BaseViewModel
    {
        private readonly Service service;
        private ObservableCollection<Article> _articles;
        private Article _selectedArticle;
        private bool _loading;
        public Article SelectedArticle { get; set; }
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
        public ICommand LoadArticles { get; private set; }
        public ICommand SelectArticle { get; private set; }

        public TopHeadlinesViewModel()
        {
            _loading = true;
            service = new Service();
            _articles = new ObservableCollection<Article>();
            LoadArticles = new Command(async () => await GetNews());
            SelectArticle = new Command(async () => await LoadArticle());
        }

        private async Task GetNews()
        {
            var news = new ObservableCollection<Article>();
            var url = Constants.TopStories;
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
            if (SelectedArticle == null)
                return;
            await service.NavigationPushAsync(new WebPage(SelectedArticle.Url));
            SetValue(ref _selectedArticle, null);
            OnPropertyChanged(nameof(SelectedArticle));
        }
    }
}
