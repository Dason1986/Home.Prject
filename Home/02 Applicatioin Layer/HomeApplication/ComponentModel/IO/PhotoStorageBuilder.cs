using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using Library.Draw;
using Library.HelperUtility;
using Library.Storage.Image;

namespace HomeApplication.ComponentModel.IO
{

    public class PhotoStorageBuilder
    {
        public PhotoStorageBuilder()
        {
            ThumbnailHasBorder = true;
        }

        public Image SourceImage { get; set; }
        public bool IsPanoramic { get; set; }
        public bool ThumbnailHasBorder { get; set; }
        public Orientation Orientation { get; set; }

        public IImageStorage Storage { get; set; }

        private Size[] LvSizes = new[] { new Size(1024, 780), new Size(2400, 1920) };

        public int Build()
        {
         
            int maxLive = 0;
            {
                var builder = new PhotoThumbnailBuilder( )  ;
                builder.SetInfo(this);
                var lv = builder.Create();
                Storage.AddThumbnail(lv);
            }
            if (IsPanoramic)
            {
                var builder = new PhotoPanoramicBuilder()  ;
                builder.SetInfo(this);
                maxLive = 1;
                var lv = builder.Create();
                Storage.Add(lv, maxLive);
            }
            else
            {
                var builder = new PhotoZoomBuilder( )  ;
                builder.SetInfo(this);
                for (var i = 0; i < LvSizes.Length; i++)
                {
                    var item = LvSizes[i];
                    if (compar(SourceImage.Size, item) <= 0) continue;
                    maxLive = i + 1;
                    builder.ZoomSize = item;
                    var lv = builder.Create();
                    Storage.Add(lv, maxLive);
                }
                if (maxLive == 0)
                {
                    maxLive = 1;
                    builder.ZoomSize = LvSizes[0];
                    var lv = builder.Create();
                    Storage.Add(lv, maxLive);
                }
            }
            return maxLive;
        }

        private int compar(Size x, Size y)
        {
            if (x.Width == y.Width && x.Height == y.Height) return 0;
            return (x.Width * x.Height).CompareTo(y.Width * y.Height);
        }

    }


}