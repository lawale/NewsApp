using System;
using System.Collections.Generic;
using System.Text;
using NewsApp.Model;

namespace NewsApp.ViewModel
{
    public class LinkViewModel
    {
        private readonly Uri _url;
        private Link UrlLink;
        public LinkViewModel(string url)
        {
            _url = new Uri(url);
            UrlLink = new Link { DomainName = _url.DnsSafeHost, Url = _url.AbsoluteUri };
        }
        public Link Link { get => UrlLink;
            set { UrlLink = value; }
        }
    }
}