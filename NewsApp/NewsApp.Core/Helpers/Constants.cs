using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Helpers
{
    public static class Constants
    {
        private const string Headlines = Secrets.Headlines;
        private const string Others = Secrets.Others;
        private const string ApiKey = Secrets.ApiKey;
        public static string TopStories
        {
            get
            {
                return string.Format(Headlines,"?", ApiKey);
            }
        }

        public static string CategoryStories(string cat)
        {
            return string.Format(Others, "?", ApiKey);
            //return string.Format(Others, "?", cat, "&", ApiKey);
        }
    }
}
