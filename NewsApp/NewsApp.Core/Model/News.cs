using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NewsApp.Core.Model
{
    public class News
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public ObservableCollection<Article> Articles { get; set; }
    }

    public class Source
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Article
    {
        private static int count = 0;
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public string ErrorImage { get; }
        public string PlaceHolder { get; }
        public DateTime PublishedAt { get; set; }

        public Article()
        {
            if (count % 2 == 0)
                ErrorImage = "Error_Image_One";
            else
                ErrorImage = "Error_Image_Two";
        }
    }
}
