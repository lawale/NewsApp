using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Helpers
{
    public static class Constants
    {
        private const string HeadLines = "https://newsapi.org/v2/top-headlines?country=ng&apiKey={0}";
        private const string Others = "https://newsapi.org/v2/top-headlines?country=ng&category={0}&apiKey={1}";
        private const string APiKey = "9b11455b30834425a6c30b0770afd7fd";
        public static string TopStories
        {
            get
            {
                return string.Format(HeadLines, APiKey);
            }
        }

        public static string CategoryStories(string cat)
        {
            return string.Format(Others, cat, APiKey);
        }
    }
}
