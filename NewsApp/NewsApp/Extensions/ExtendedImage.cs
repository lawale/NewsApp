using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NewsApp.Extensions
{
    class ExtendedImage : FFImageLoading.Forms.CachedImage
    {
        public bool HasDownloaded { get; set; }
        public bool DownloadFailed { get; set; }

        public ExtendedImage()
        {
            var recognizer = new TapGestureRecognizer
            {
                Command = new Command(RetryDownload),
                NumberOfTapsRequired = 1
            };
            GestureRecognizers.Add(recognizer);
        }

        void RetryDownload()
        {
            if (!HasDownloaded && DownloadFailed)
                ReloadImage();
        }
    }
}
