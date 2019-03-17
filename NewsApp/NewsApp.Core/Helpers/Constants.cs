using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Helpers
{
    public static class Constants
    {
        private const string HeadLines = Secrets.HeadLines;
        private const string Others = Secrets.Others;
        private const string APiKey = Secrets.APiKey;
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
