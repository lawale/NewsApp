using NewsApp.Extensions;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NewsApp.ViewModel
{
    public class WebViewModel : BaseViewModel
    {
        private string _url;
        private bool _loading;
        private bool _isBookmarked;
        private readonly IServices services;
        private readonly LinkViewModel Link;
        public ICommand ShareCommand;
        public ICommand OpenBrowserCommand;
        public ICommand BookmarkCommand;
        public ICommand SearchPageCommand;
        public ICommand NavigatingCommand;
        public ICommand RefreshPageCommand;
        public ICommand GoBackCommand { get; set; }
        public ICommand GoForwardCommand { get; set; }
        public bool CanGoBack { get; set; }
        public bool CanGoForward { get; set; }

        public bool IsBoomarked
        {
            get => _isBookmarked;
            set { _isBookmarked = value; }
        }

        public string Url
        {
            get => _url;
            set { _url = value; }
        }

        public bool Loading
        {
            get => _loading;
            set { _loading = value; }
        }


        public void SetLoading(bool value)
        {
            SetValue(ref _loading, value);
            OnPropertyChanged(nameof(Loading));
            _isBookmarked = services.GetBookmarks(Link);
        }

        public WebViewModel(string url, IServices service)
        {
            Link = new LinkViewModel(url);
            services = service;
            _url = url;
            _loading = true;
            ShareCommand = new Command(Share);
            OpenBrowserCommand = new Command(OpenBrowser);
            //RefreshPageCommand = new Command();
            //BookmarkCommand = new Command();
            //SearchPageCommand = new Command();
        }

        private void OpenBrowser()
        {
            if (!CrossShare.IsSupported)
                return;
            if (CrossShare.Current.CanOpenUrl(_url))
            {
                CrossShare.Current.OpenBrowser(_url);
            }
        }

        private void RefreshPage()
        {
            throw new NotImplementedException();
        }

        private void BookmarkPage()
        {
            if (_isBookmarked)
                services.RemoveBookmark(Link);
            else
                services.BookmarkLink(Link);
        }

        private void SearchPage()
        {
            throw new NotImplementedException();
        }

        private void Share()
        {
            if (!CrossShare.IsSupported)
                return;
            var shareUri = new Uri(_url);
            CrossShare.Current.Share(new ShareMessage
            { Title = "Check out this story", Text = $"Read this story on {shareUri.DnsSafeHost}", Url = _url },
            new ShareOptions
            { ChooserTitle = "Share Story" });
        }
    }
}
