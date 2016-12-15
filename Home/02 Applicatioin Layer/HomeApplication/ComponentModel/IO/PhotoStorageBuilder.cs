using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using Library.Draw;
using Library.IO.Storage.Image;
using Library.HelperUtility;

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

        Size[] LvSizes = new[] { new Size(1024, 780), new Size(2400, 1920) };

        public int Build()
        {
            SourceRectangle = new Rectangle(0, 0, SourceImage.Width, SourceImage.Height);
            int maxLive = 0;
            {
                var lv = CreateImageThumbnail(SourceImage);
                Storage.Update(lv, 0);
            }
            if (IsPanoramic)
            {
                maxLive = 1;
                var lv = CreateImagePanoramic(SourceImage);
                Storage.Update(lv, maxLive);
            }
            else
            {
                for (var i = 0; i < LvSizes.Length; i++)
                {
                    var item = LvSizes[i];
                    if (compar(SourceImage.Size, item) > 0)
                    {
                        maxLive = i + 1;
                        var lv = CreateImage(SourceImage, item);
                        Storage.Update(lv, maxLive);
                    }
                }
                if (maxLive == 0)
                {
                    maxLive = 1;
                    var lv = CreateImage(SourceImage, LvSizes[0]);
                    Storage.Update(lv, maxLive);
                }
            }
            return maxLive;
        }

        int compar(Size x, Size y)
        {
            if (x.Width == y.Width && x.Height == y.Height) return 0;
            return (x.Width * x.Height).CompareTo(y.Width * y.Height);
        }
        const int MamarginLR=10;
        const int MamarginTB=230;
        static readonly RectangleF ThumbnailFillRectangle = new RectangleF(MamarginLR, MamarginLR, MamarginTB, MamarginTB);
        const int ThumbnailPixels = 234;

        Rectangle SourceRectangle;


        protected Stream CreateImageThumbnail(Image image)
        {
            var min = Math.Min(image.Width, image.Height);
            var max = Math.Max(image.Width, image.Height);
            Bitmap tempImage = new Bitmap(250, 250, PixelFormat.Format64bppArgb);
             
            Graphics g = Graphics.FromImage(tempImage);
            g.Clear(Color.White);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if (ThumbnailHasBorder)
            {
                g.DrawRectangle(new Pen(Color.Gray, 4), 8, 8, 234, 234);
                g.DrawRectangle(new Pen(Color.LightGray, 2), 2, 2, 246, 246);
            }
            if (min < 250)
            {


                if (min == image.Width)
                {
                    if (max > ThumbnailPixels)
                    {
                        g.DrawImage(image, ThumbnailFillRectangle, SourceRectangle, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        var per = (decimal)ThumbnailPixels / image.Height;
                        var tempWidth = (int)(image.Width * per);
                        var paddingleft = (ThumbnailPixels - tempWidth) / 2;
                        g.DrawImage(image, new Rectangle(paddingleft,12,  tempWidth, 226), SourceRectangle, GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    if (min > ThumbnailPixels)
                    {
                        g.DrawImage(image, ThumbnailFillRectangle, SourceRectangle, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        var per = (decimal)ThumbnailPixels / image.Width;


                        var tempHeight = (int)(image.Height * per);
                        var paddingTop = (ThumbnailPixels - tempHeight) / 2;
                        g.DrawImage(image, new Rectangle(12, paddingTop, 226, tempHeight), SourceRectangle, GraphicsUnit.Pixel);
                    }
                }

            }
            else
            {

                g.DrawImage(image, ThumbnailFillRectangle, SizeUtility.GetSquareRectangle(image.Size), GraphicsUnit.Pixel);

            }

            g.Dispose();

            switch (Orientation)
            {
                case Library.Draw.Orientation.Rotate180:
                    tempImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case Library.Draw.Orientation.Rotate270CW:
                    tempImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case Library.Draw.Orientation.Rotate90CW:
                    tempImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                default:
                    break;
            }
            MemoryStream ms = new MemoryStream();
            tempImage.Save(ms, ImageFormat.Jpeg);

            tempImage.Dispose();
            return ms;

        }
        protected Stream CreateImage(Image image, Size size)
        {

            Size endPoint = image.Size;
            if ((image.Width < size.Width && image.Height < size.Height) == false)
            {



                endPoint = SizeUtility.ZoomSizeByPixels(endPoint, image.Width > image.Height ? size.Height : size.Width);

            }



            Bitmap tempImage = new Bitmap(endPoint.Width, endPoint.Height, PixelFormat.Format64bppArgb);


            Graphics g = Graphics.FromImage(tempImage);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, new Rectangle(Point.Empty, endPoint), SourceRectangle, GraphicsUnit.Pixel);



            g.Dispose();
            switch (Orientation)
            {
                case Library.Draw.Orientation.Rotate180:
                    tempImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case Library.Draw.Orientation.Rotate270CW:
                    tempImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case Library.Draw.Orientation.Rotate90CW:
                    tempImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                default:
                    break;
            }
            MemoryStream ms = new MemoryStream();
            tempImage.Save(ms, ImageFormat.Jpeg);

            tempImage.Dispose();
            return ms;
        }
        protected Stream CreateImagePanoramic(Image image)
        {
            const int fixHeight = 1080;
            var tempheight = image.Size.Height > fixHeight ? fixHeight : image.Size.Height;
            var per = (decimal)tempheight / image.Height;
            var tempWidth = (int)(image.Width * per);
            Bitmap tempImage = new Bitmap(tempWidth, tempheight, PixelFormat.Format64bppArgb);


            Graphics g = Graphics.FromImage(tempImage);
            g.DrawImage(image, new RectangleF(0, 0, tempWidth, tempheight), SourceRectangle, GraphicsUnit.Pixel);
            g.Dispose();
            MemoryStream ms = new MemoryStream();
            tempImage.Save(ms, ImageFormat.Jpeg);

            tempImage.Dispose();
            return ms;
        }
    }
}