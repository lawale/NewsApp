using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Extensions
{
    class ExtendedImage : FFImageLoading.Forms.CachedImage
    {
        public bool HasDownloaded { get; set; }
        public bool DowloadFailed { get; set; }
    }
}
