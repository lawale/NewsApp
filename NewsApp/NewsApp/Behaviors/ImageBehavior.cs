using System;
using System.Collections.Generic;
using System.Text;
using FFImageLoading.Forms;
using NewsApp.Extensions;
using Xamarin.Forms;

namespace NewsApp.Behaviors
{
    class ImageBehavior : Behavior<ExtendedImage>
    {
        protected override void OnAttachedTo(ExtendedImage bindable)
        {
            bindable.Success += ImageDownload;
            bindable.Error += DownloadError;
            base.OnAttachedTo(bindable);
        }

        private void DownloadError(object sender, CachedImageEvents.ErrorEventArgs e)
        {
            var image = sender as ExtendedImage;
            
            image.DownloadFailed = true;
            image.HasDownloaded = false;
        }

        private void ImageDownload(object sender, CachedImageEvents.SuccessEventArgs e)
        {
            var image = sender as ExtendedImage;
            image.HasDownloaded = true;
            image.DownloadFailed = false;
        }

        protected override void OnDetachingFrom(ExtendedImage bindable)
        {
            bindable.Success -= ImageDownload;
            bindable.Error -= DownloadError;
            base.OnDetachingFrom(bindable);
        }
    }
}
