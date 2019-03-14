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
        #region backingfields
        private readonly NewsCategory category;
        private readonly IServices services = DependencyService.Get<IServices>();
        private ObservableCollection<Article> articles;
        private bool isRefreshing;
        private IToast Toast = DependencyService.Get<IToast>();
        private bool hasLoaded;
        #endregion

        #region properties
        public ObservableCollection<Article> Articles
        {
            get => articles;
            private set => SetValue(ref articles, value);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetValue(ref isRefreshing, value);
        }

        public bool HasLoaded
        {
            get => hasLoaded;
            set => SetValue(ref hasLoaded, value);
        }

        public string Title => category.CategoryName.ToUpper();

        #endregion

        #region commands
        public ICommand SelectArticle { get; }
        public ICommand RefreshArticles { get; }
        public ICommand ReadArticleCommand { get; }
        #endregion

        #region contructor
        public NewsViewModel(NewsCategory category)
        {
            this.category = category;
            articles = new ObservableCollection<Article>();
            RefreshArticles = new Command(async () => await Refresh());
            ReadArticleCommand = new Command<Article>(async vm => await ReadArticle(vm));
        }
        #endregion
        
        #region private methods and method groups

        private async Task<bool> GetNews()
        {
            var news = new ObservableCollection<Article>();
            string url;
            if (category.CategoryName.ToLower() == topstories)
                url = Constants.TopStories;
            else
                url = Constants.CategoryStories(category.CategoryName.ToLower());
            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.None)
            {
                Toast.Show("Check your Internet Connection!");
                return false;
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
                            return false;
                        }
                        news = result.Articles;
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine(e.Source);
                    //Console.WriteLine(e.Message);
                    Toast.Show("An error occured!");
                    return false;
                }
            }


            var empty = Articles.Where(x => x.UrlToImage == null);
            foreach (var image in empty)
            {
                image.UrlToImage = null;
            }
            Articles.Clear();
            foreach (var article in news)
                Articles.Add(article);
            return true;
        }
        
        /// <summary>
        /// Gets the latest news item from the endpoint
        /// </summary>
        /// <returns>Returne a task</returns>
        private async Task Refresh()
        {
            HasLoaded = false;
            IsRefreshing = true;
            await GetNews();
            IsRefreshing = false;
            HasLoaded = true;
        }

        /// <summary>
        /// Action method for executing the read article button command
        /// </summary>
        /// <param name="article">the article item to be opened</param>
        /// <returns>return a task</returns>
        private async Task ReadArticle(Article article)
        {
            await Xamarin.Essentials.Browser.OpenAsync(article.Url, Xamarin.Essentials.BrowserLaunchMode.SystemPreferred);
        }
        #endregion
    }
}
